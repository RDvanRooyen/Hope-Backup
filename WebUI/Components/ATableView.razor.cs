using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebUI.Data.Interfaces;
using WebUI.Services;

namespace WebUI.Components
{
    public partial class ATableView<TItem, TService>
        where TItem : class
        where TService : ServiceBase
    {

        #region parameters

        [Parameter]
        public EventCallback<TItem> OnItemEdit { get; set; }

        [Parameter]
        public Expression<Func<TItem, bool>> IdFieldFilter { get; set; }


        [Parameter]
        public RenderFragment ToolbarContent { get; set; }
        [Parameter]
        public RenderFragment SecondRowToolbar { get; set; }
        [Parameter]
        public RenderFragment PagerToolbar { get; set; }
        [Parameter]
        public bool ToolbarOneLine { get; set; }


        /// <summary>
        /// A comma-delimited list of all roles that are allowed to display the content
        /// defaults to all roles
        /// </summary>
        [Parameter]
        public string Roles { get; set; } = "SuperAdmin,Admin";

        [Parameter]
        public string AdditionalCSSClasses { get; set; }

        /// <summary>
        /// how manay items per page, defaults to 15
        /// </summary>
        [Parameter]
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// the display title for this list page
        /// </summary>
        [Parameter]
        public string @Title { get; set; }

        /// <summary>
        /// the expression for getting the ID of each item in the list
        /// </summary>
        [Parameter]
        public Expression<Func<TItem, object>> IdField { get; set; }

        /// <summary>
        /// the expression for getting the ID of each item in the list
        /// </summary>
        [Parameter]
        public Expression<Func<TItem, int>> EditIdField { get; set; }

        /// <summary>
        /// the expression for getting the desired display name of each item in the list
        /// </summary>
        [Parameter]
        public Expression<Func<TItem, object>> NameField { get; set; }

        /// <summary>
        /// an optional string title for the NameField (e.g. Description), defaults to "Name"
        /// </summary>
        [Parameter]
        public string NameFieldTitle { get; set; } = "Name";

        /// <summary>
        /// an optional flag to sort on the name column, defaults to true
        /// </summary>
        [Parameter]
        public bool SortOnName { get; set; } = true;

        /// <summary>
        /// the expression for filtering, including subtypes, or otherwise manipulating
        /// the dataset used to populate the list view
        /// </summary>
        [Parameter]
        public Func<IQueryable<TItem>, IQueryable<TItem>> Filter { get; set; }

        /// <summary>
        /// UI (html) segment to render additional columns into the list table
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// the "type" part of the url to the editor page for these items.
        /// Example: the "participant" in /manage/participant/{id}
        /// </summary>
        [Parameter]
        public string ManagePath { get; set; }

        [Parameter]
        public string ControlPrefix { get; set; }
        #endregion

        // flags for confirmation dialog actions
        private bool _confDialogIsOpen;

        [Parameter]
        public Func<TItem, bool> InitialFilterPredicate { get; set; }

        [Parameter]
        public bool AlwaysShow { get; set; } = true;

        public Func<TItem, bool> FilterPredicate { get; set; }

        // items to actually be displayed, which could be AllItems or a subset
        private ICollection<TItem> _displayedItems
        {
            get
            {
                if (AllItems != null)
                {
                    var items = AllItems;

                    if (_itemsAreActivable)
                    {
                        if (_showActive && _showInactive)
                        {
                            items = AllItems;
                        }
                        else if (_showActive)
                        {
                            items = AllItems.Where(x => ((ISoftDeletable)x).IsActive).ToList();

                        }
                        else if (_showInactive)
                        {
                            //hide inactive
                            items = AllItems.Where(x => !((ISoftDeletable)x).IsActive).ToList();
                        }
                        else
                        {
                            items = new List<TItem>();
                        }
                    }

                    if (FilterPredicate != null)
                    {
                        items = items.Where(FilterPredicate).ToList();
                    }
                    return items;
                }
                else
                {
                    return null;
                }


            }
        }

        // convenience property tells us if there are any soft-deleted records
        //private bool _hasDeletedItems => _itemsAreSoftDeletable ? AllItems.Where(x => ((ISoftDeletable)x).Deleted).Any() : false;

        // holds on to an item selected for deletion while confirming
        private TItem _itemToDelete;

        // holds on to an item selected for editing
        private TItem _itemToEdit;

        // a flag to show deleted records in the list view
        private bool _showDeleted;

        private string _deleteRestoreButtonAction => _showDeleted ? "restore" : "delete";

        // is TItem an ISoftDeletable?
        private bool _itemsAreSoftDeletable;

        // the service for all DB ops
        public TService Service { get; set; }

        // the full item list for this list view
        [Parameter]
        public List<TItem> AllItems { get; set; }

        [Parameter]
        public bool UseAllItems { get; set; }

        [Parameter]
        public bool ShowActiveToggle { get; set; } = true;

        public bool _itemsAreActivable;

        bool _showActive = true;
        bool _showInactive = false;


        // load data before rendering
        protected override async Task OnInitializedAsync()
        {
            // knowing the ID field is an absolute must
            if (IdField == null)
            {
                throw new ArgumentNullException("Management List Page requires an ID field");
            }

            // get service
            Service = await _factory.CreateService<TService>(_authenticationStateProvider);

            if (!UseAllItems)
            {
                // get queryable collection of generic type TItem, passing in optional Filter arg
                AllItems = await Service.GetQueryableIncludingDeleted<TItem>(asUntracked: false, filter: Filter).ToListAsync();
            }
            // check if the assigned type is deletable
            //_itemsAreSoftDeletable = typeof(TItem).GetInterfaces().Contains(typeof(ISoftDeletable));
            var k = typeof(TItem).GetInterfaces();
            _itemsAreActivable = k.Contains(typeof(IAuditable)); ;
            FilterPredicate = InitialFilterPredicate;
        }

        public async Task RefreshItems(List<TItem> list = null)
        {
            FilterPredicate = null;


            if (!UseAllItems)
            {
                Service = await _factory.CreateService<TService>(_authenticationStateProvider);
                AllItems = await Service.GetQueryableIncludingDeleted<TItem>(asUntracked: false, filter: Filter).ToListAsync();
            }
            else
            {
                AllItems = list;
            }

        }

        public async Task FilterItems(Func<TItem, bool> f)
        {
            FilterPredicate = f;
        }

        void RowClickAction(TItem item)
        {
        }

        // edit using dialog
        async Task EditClick(TItem item)
        {
            await OnItemEdit.InvokeAsync(item);
        }
        // Delete or restore an item when the appropriate button is clicked
        //private void DeleteOrRestoreClick(TItem item)
        //{
        //    if (!_showDeleted)
        //    {
        //        // open the delete confirmation dialog
        //        _itemToDelete = item;
        //        _confDialogIsOpen = true;
        //    }
        //    else if (_itemsAreSoftDeletable)
        //    {
        //        Service.Restore((ISoftDeletable)item);
        //        string msg = String.Format("{0} restored successfully", GetItemName(item));
        //        _toaster.Add(msg, MatToastType.Success);
        //        // if the last deleted record was restored...
        //        if (!_hasDeletedItems)
        //        {
        //            // ...take the user back to the non-deleted list
        //            _showDeleted = false;
        //        }
        //    }
        //    else
        //    {
        //        // should never get here
        //        _toaster.Add("These items are not restorable", MatToastType.Danger);
        //    }
        //}

        // delete the selected item
        //private void Delete()
        //{
        //    CloseDialog();
        //    Service.Delete(_itemToDelete);
        //    if (!_itemsAreSoftDeletable)
        //    {
        //        // remove hard deletes from the local list just for visual consistency
        //        AllItems.Remove(_itemToDelete);
        //    }
        //    string msg = String.Format("{0} deleted successfully", GetItemName(_itemToDelete));
        //    _toaster.Add(msg, MatToastType.Success);
        //}

        private void CloseDialog()
        {
            _confDialogIsOpen = false;
        }

        // get text to show when asking the user to delete an item
        private string GetConfirmationText()
        {
            string s = "Are you sure you want to delete {0}?";
            return String.Format(s, _itemToDelete == null ? "this item"
                : GetItemName(_itemToDelete));
        }

        // get the display name for a single item in the list
        private string GetItemName(TItem item)
        {
            if (item == null) return null;

            return NameField?.Compile()?.Invoke(item)?.ToString();
        }
    }

}

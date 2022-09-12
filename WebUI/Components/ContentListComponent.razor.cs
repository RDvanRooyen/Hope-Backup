using BlazorTable;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Components.Abstract;
using WebUI.Data;
using WebUI.Data.Interfaces;
using WebUI.Data.Models;
using WebUI.Services;
using WebUI.Shared;
using WebUI.ViewModels;

namespace WebUI.Components
{
    public partial class ContentListComponent
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        [Parameter]
        public string ContentCategory { get; set; }
        ServiceBase Service { get; set; }

        private string userId;
        private List<Data.Models.Subject> _subjects;
        private List<Data.Models.Grade> _grades;
        private Dictionary<int, List<ContentDetails>> _newContentsDictionary = new Dictionary<int, List<ContentDetails>>();

        List<ContentDetails> _featuredList = new();
        List<ContentDetails> _newContents = new();

        public const string ContentNew = "new";
        public const string ContentSearch = "search";
        public const string ContentSearchResult = "result";

        private string SearchKeyword { get; set; }
        private int? SelectedSubject { get; set; }
        private int? SelectedGrade { get; set; }
        private string SelectedSortBy = "Featured";
        private int[] selectedGradeId;
        private int[] selectedSubjectId;
        private string[] SortByOptions = new[] {
        "Featured",
        "Oldest Date",
        "Newest Date",
        "Lowest Rating",
        "Highest Rating",
        };

        bool ShowAsGrid = true;
        private bool _filterDrawerShow = true;

        private int[] PageSizes = new int[] { 5, 20, 50, 100, 150, 200 };

        [Parameter]
        public SelectionType SelectionType { get; set; } = SelectionType.Multiple;
        // the set of options that the user has selected
        private List<SelectOption<int>> _selectedItems = new List<SelectOption<int>>();
        private List<ContentDetails> _searchResult = new List<ContentDetails>();
        private bool _isSearchLoading;

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
            this.userId = Service.GetCurrentUser().Id;
            _subjects = Service.GetContext().Subjects.OrderBy(x => x.Id).ToList();
            _grades = Service.GetContext().Grades.OrderBy(x => x.Id).ToList();

        }
        protected override async Task OnParametersSetAsync()
        {
            if (ContentCategory == ContentNew)
            {
                _newContentsDictionary = new Dictionary<int, List<ContentDetails>>();
                foreach (var s in _subjects)
                {
                    var topFour = Service.GetContext().ContentSubjects
                        .Include(x => x.ContentDetails)
                        .ThenInclude(x => x.ContentFiles)
                        .Where(x => x.SubjectId == s.Id && x.ContentDetails.Status == "Approved")
                        .Select(x => x.ContentDetails)
                        .OrderByDescending(x => x.ChangedDateTime)
                        .ThenByDescending(x => x.CreatedDateTime)
                        .Take(4)
                        .ToList();

                    _newContentsDictionary.Add(s.Id, topFour);

                }

            }
            else if (ContentCategory == ContentSearch)
            {
                _featuredList = Service.GetContext()
                        .ContentDetails
                        .Where(x => x.Featured && x.Status == "Approved")
                        .OrderByDescending(x => x.ChangedDateTime)
                        .ThenByDescending(x => x.CreatedDateTime)
                        .Take(4)
                        .ToList();


                _newContents = Service.GetContext()
                        .ContentDetails
                        .Where(x=>x.Status == "Approved")
                        .OrderByDescending(x => x.ChangedDateTime)
                        .ThenByDescending(x => x.CreatedDateTime)
                        .Take(4)
                        .ToList();
            }
            else if (ContentCategory == ContentSearchResult)
            {
                string k = "";
                string sort = "";
                string s = "";
                string g = "";
                int v = -1;

                /// RESET variable from previous page
                SelectedGrade = null;
                SelectedSubject = null;
                ShowAsGrid = true;
                SelectedSortBy = SortByOptions.FirstOrDefault();

                this.selectedGradeId = Array.Empty<int>();
                this.selectedSubjectId = Array.Empty<int>();
                if (NavManager.TryGetQueryString<string>("k", out k))
                {
                    SearchKeyword = k;
                }
                if (NavManager.TryGetQueryString<string>("s", out s))
                {
                    //SelectedSubject = s;
                    selectedSubjectId = s.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                }
                if (NavManager.TryGetQueryString<string>("g", out g))
                {
                    //SelectedGrade = g;
                    selectedGradeId = g.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                }
                if (NavManager.TryGetQueryString<string>("sort", out sort))
                {
                    SelectedSortBy = sort;
                }
                if (NavManager.TryGetQueryString<int>("grid", out v))
                {
                    ShowAsGrid = v == 1;
                }

                foreach (var i in _grades)
                {
                    i.Checked = selectedGradeId.Contains(i.Id);
                }
                foreach (var i in _subjects)
                {
                    i.Checked = selectedSubjectId.Contains(i.Id);
                }

                await PerformSearchAsync();
            }

            MainLayout.HideProgressBar();

        }

        private int _pageSize = 5;
        private int _page = 0;
        private int _take = 5;
        private int _totalFound = 0;
        private int _pageCount = 1;
        private async Task PerformSearchAsync()
        {
            _isSearchLoading = true;
            var q = Service.GetContext().ContentDetails
                .Where(x => x.Status == "Approved")
                .Include(x => x.ContentGrades)
                .Include(x => x.ContentSubjects)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchKeyword))
            {
                q = q.Where(x => x.Title.Contains(SearchKeyword) || x.SummaryText.Contains(SearchKeyword));
            }

            if (selectedGradeId.Any())
            {
                q = q.Where(x =>
                    x.ContentGrades.Where(c => selectedGradeId.Contains(c.GradeId)).Any()
                    );
            }
            if (selectedSubjectId.Any())
            {
                q = q.Where(x =>
                    x.ContentSubjects.Where(c => selectedSubjectId.Contains(c.SubjectId)).Any()
                    );
            }

            _totalFound = await q.CountAsync();
            _pageCount = (int)Math.Ceiling(((decimal)_totalFound) / ((decimal)_pageSize));
            _take = _pageSize;
            int skip = _page * _take;

            switch (SelectedSortBy)
            {
                case "Featured":
                    q = q.OrderBy(x => x.Featured).ThenBy(x => x.CreatedDateTime);
                    break;
                case "Oldest Date":
                    q = q.OrderBy(x => x.CreatedDateTime).ThenBy(x => x.CreatedDateTime);
                    break;
                case "Newest Date":
                    q = q.OrderByDescending(x => x.CreatedDateTime);
                    break;
                case "Lowest Rating":
                    q = q.OrderBy(x => x.AverageRating);
                    break;
                case "Highest Rating":
                    q = q.OrderByDescending(x => x.AverageRating).ThenBy(x => x.CreatedDateTime);
                    break;
            }

            _searchResult = await q.Skip(skip).Take(_take).ToListAsync();
            _isSearchLoading = false;
        }

        private void ShowContentDetail(int contentDetailId)
        {
            NavManager.NavigateTo("/content/view/" + contentDetailId, forceLoad: true);
        }
        public void SearchForContent()
        {
            NavManager.NavigateTo($"/content/list/result?k={SearchKeyword}&s={SelectedSubject}&g={SelectedGrade}&sort={SelectedSortBy}&grid={(ShowAsGrid ? "1" : "0")}", forceLoad: true);
        }


        private void SearchAsGrid()
        {
            ShowAsGrid = true;
            ApplyFilter();
        }

        private void SearchAsList()
        {
            ShowAsGrid = false;
            ApplyFilter();
        }

        private void ShowFilterDrawer()
        {
            _filterDrawerShow = !_filterDrawerShow;
        }

        private bool HasFilter
        {
            get
            {

                return !string.IsNullOrEmpty(SearchKeyword)
                    || _grades.Where(x => x.Checked ?? false).Any()
                    || _subjects.Where(x => x.Checked ?? false).Any();
            }
        }

        private string SelectedGrades
        {
            get
            {
                var l = _grades.Where(x => x.Checked ?? false).Select(x => x.Id).ToArray();
                return string.Join(',', l);
            }
        }
        private string SelectedSubjects
        {
            get
            {
                var l = _subjects.Where(x => x.Checked ?? false).Select(x => x.Id).ToArray();
                return string.Join(',', l);
            }
        }

        private void ApplyFilter()
        {
            NavManager.NavigateTo($"/content/list/result?k={SearchKeyword}&s={SelectedSubjects}&g={SelectedGrades}&sort={SelectedSortBy}&grid={(ShowAsGrid ? "1" : "0")}");
        }

        private void ClearFilter()
        {
            foreach (var i in _grades)
            {
                i.Checked = false;
            }
            foreach (var i in _subjects)
            {
                i.Checked = false;
            }
            SearchKeyword = string.Empty;
        }



        public async Task ShowPageAsync(int p)
        {
            _page = p - 1;
            await PerformSearchAsync();
        }

        public async Task PageSizeChangedAsync(ChangeEventArgs e)
        {
            _pageCount = 1;
            _page = 0;
            _pageSize = Convert.ToInt32(e.Value);
            await PerformSearchAsync();
        }

        public async Task SortByChangedAsync(ChangeEventArgs e)
        {
            SelectedSortBy = Convert.ToString(e.Value);
            await PerformSearchAsync();
        }


    }
}

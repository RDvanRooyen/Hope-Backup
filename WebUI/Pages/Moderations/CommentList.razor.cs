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


namespace WebUI.Pages.Moderations
{
    public partial class CommentList
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        [Parameter]
        public string ModerationCategory { get; set; }
        ServiceBase Service { get; set; }

        private bool _deleteDialogIsOpen;

        List<ContentComment> _comments
        {
            get
            {
                if (Service != null)
                {
                    var c = Service.GetContext().ContentComments
                        .Include(x => x.User)
                        .Include(x => x.ContentDetails)
                        .Where(x => x.Approved == false && x.ApprovedDate == null)
                        .OrderBy(x => x.Added)
                        .ToList();
                    return c;
                }
                else
                {
                    return new();
                }
            }
        }
        ContentComment _comment { get; set; }
        int _rejectCommentId;
        string _rejectCommentName;

        public const string UserModeration = "users";
        public const string ContentModeration = "contents";
        public const string CommentModeration = "comments";

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
        }

        protected override void OnParametersSet()
        {   
        }
        public void ApproveComment(int id)
        {
            var uid = Service.GetCurrentUser().Id;
            var c = Service.GetContext().ContentComments
                    .Where(x => x.Id == id).FirstOrDefault();
            c.Approved = true;
            c.ApprovedDate = DateTime.Now;
            c.ModeratorId = uid;
            Service.SaveChanges();
            //bool result = false;
            //try
            //{
            //    _comment = Service.GetContext().ContentComments
            //        .Where(x => x.Id == id).FirstOrDefault();
            //    _comment.Approved = true;
            //    _comment.ApprovedDate = DateTime.Now;
            //    Service.GetContext().SaveChanges();
            //    StateHasChanged();

            //    result = true;
            //}
            //catch
            //{

            //}
            //return result;
        }
        public void RejectComment(int id)
        {
            var uid = Service.GetCurrentUser().Id;
            var c = Service.GetContext().ContentComments
                    .Where(x => x.Id == id).FirstOrDefault();
            c.Approved = false;
            c.ApprovedDate = DateTime.Now;
            c.ModeratorId = uid;
            Service.SaveChanges();
        }

        public void ApproveSelectedComments()
        {
            var uid = Service.GetCurrentUser().Id;
            var selectedComments = _comments.Where(x => x.Checked);
            foreach (var c in selectedComments)
            {
                c.Approved = true;
                c.ApprovedDate = DateTime.Now;
                c.ModeratorId = uid;
            }

            Service.SaveChanges();
        }

        public void RejectSelectedComments()
        {
            var uid = Service.GetCurrentUser().Id;
            var selectedComments = _comments.Where(x => x.Checked);
            foreach (var c in selectedComments)
            {
                c.Approved = false;
                c.ApprovedDate = DateTime.Now;
                c.ModeratorId = uid;
            }

            Service.SaveChanges();
        }

        public void SelectAllComments()
        {
            foreach (var c in _comments)
            {
                c.Checked = true;
            }
        }

        public void SelectNoneComments()
        {
            foreach (var c in _comments)
            {
                c.Checked = false;
            }
        }
        private void OpenDeleteDialog()
        {
            _deleteDialogIsOpen = true;
        }
        private void CloseDeleteDialog()
        {
            _deleteDialogIsOpen = false;
        }

        private int TotalSelected
        {
            get
            {
                if (_comments == null || !_comments.Any()) return 0;

                return _comments.Where(x => x.Checked).Count();
            }
        }
    }
}

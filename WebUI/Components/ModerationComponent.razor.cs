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
    public partial class ModerationComponent
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        [Parameter]
        public string ModerationCategory { get; set; }
        ServiceBase Service { get; set; }

        private bool _deleteDialogIsOpen;

        List<ApplicationUser> _users = new();
        ApplicationUser _user { get; set; }
        string _rejectUserId;
        string _rejectUserName;

        List<ContentDetails> _contents = new();
        ContentDetails _contentDetails { get; set; }
        int _rejectContentId;
        string _rejectContentTitle;
         

        public const string UserModeration = "users";
        public const string ContentModeration = "contents";
        public const string CommentModeration = "comments";

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
        }

        protected override void OnParametersSet()
        {
            _users = new();
            _contents = new(); 

            if (ModerationCategory == UserModeration)
            {
                _users = Service.GetContext().Users
                    .Where(x => x.Status == "Submitted_Pending")
                    .Include(x => x.Grades).ThenInclude(x => x.Grade)
                    .OrderBy(x => x.Id)
                    .ToList();
            }

            if (ModerationCategory == ContentModeration)
            {
                _contents = Service.GetContext().ContentDetails
                    .Where(x => x.Status == "Submitted/Pending")
                    .Include(x => x.ContentSubjects).ThenInclude(x => x.Subject)
                    .Include(x => x.ContentGrades).ThenInclude(x => x.Grade)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                    .OrderBy(x => x.Id)
                    .ToList();
            } 
        }
        public bool ApproveUser(string id)
        {
            bool result = false;
            try
            {
                _user = Service.GetContext().Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
                _user.Status = "Approved";
                Service.GetContext().SaveChanges();
                StateHasChanged();

                result = true;
            }
            catch
            {

            }
            return result;
        }
        public bool RejectUser(string id)
        {
            bool result = false;
            try
            {
                CloseDeleteDialog();
                _user = Service.GetContext().Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
                _user.Status = "Rejected";
                Service.GetContext().SaveChanges();
                StateHasChanged();

                result = true;
            }
            catch
            {
                CloseDeleteDialog();
            }
            return result;
        }
        public bool ApproveContent(int id)
        {
            bool result = false;
            try
            {
                _contentDetails = Service.GetContext().ContentDetails
                    .Where(x => x.Id == id).FirstOrDefault();
                _contentDetails.Status = "Approved";
                Service.GetContext().SaveChanges();
                StateHasChanged();

                result = true;
            }
            catch
            {

            }
            return result;
        }
        public bool RejectContent(int id)
        {
            bool result = false;
            try
            {
                CloseDeleteDialog();
                _contentDetails = Service.GetContext().ContentDetails
                    .Where(x => x.Id == id).FirstOrDefault();
                _contentDetails.Status = "Rejected";
                Service.GetContext().SaveChanges();
                StateHasChanged();

                result = true;
            }
            catch
            {
                CloseDeleteDialog();
            }
            return result;
        } 
         
        private void OpenDeleteDialog()
        {
            _deleteDialogIsOpen = true;
        }
        private void CloseDeleteDialog()
        {
            _deleteDialogIsOpen = false;
        }
         
    }
}
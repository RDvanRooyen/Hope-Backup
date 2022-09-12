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


namespace WebUI.Pages.Contents
{

    public partial class MyContent
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        public ServiceBase Service { get; private set; }
        List<ContentDetails> _myContent = new();
        List<ContentBookmark> _bookmarks = new();

        private string userId;

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
            this.userId = Service.GetCurrentUser().Id;
            //_subjects = Service.GetContext().Subjects.OrderBy(x => x.Id).ToList();
            //_grades = Service.GetContext().Grades.OrderBy(x => x.Id).ToList();

        }
        protected override async Task OnParametersSetAsync()
        {
            _myContent = Service.GetContext().ContentDetails.
                      Where(x => x.CreatedById.Equals(userId)).OrderBy(x => x.Id).ToList();

            _bookmarks = Service.GetContext().ContentBookmarks.Include(x => x.ContentDetails).
                Where(x => x.UserId.Equals(userId)).OrderBy(x => x.ContentDetailsId).ToList();

            MainLayout.HideProgressBar();
        }
        private void SelectBookmark(ChangeEventArgs e, ContentBookmark d)
        {
            var s = Convert.ToBoolean(e.Value);
            d.Shared = s;
            Service.SaveChanges();
        }

        public int _bookmarkChecked
        {
            get
            {
                if (_bookmarks == null) return 0;

                var selected = _bookmarks.Where(x => x.Shared).Count();
                return selected;
            }
        }
    }
}
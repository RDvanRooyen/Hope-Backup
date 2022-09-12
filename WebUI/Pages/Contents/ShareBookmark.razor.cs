using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data;
using WebUI.Data.Models;
using WebUI.Data.ViewModels;
using WebUI.Helpers;
using WebUI.Services;

namespace WebUI.Pages.Contents
{
    public partial class ShareBookmark
    {
        private List<ContentBookmark> _bookmarks;

        [Parameter]
        public string UID { get; set; }
        public ServiceBase Service { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);

        }

        protected override async Task OnParametersSetAsync()
        {
            _bookmarks = Service.GetContext().ContentBookmarks.Include(x=>x.ContentDetails).Where(x => x.UserId == UID && x.Shared).ToList();
        }
    }
}

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
    public partial class ContentList
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        [Parameter]
        public string ModerationCategory { get; set; }
        ServiceBase Service { get; set; }

        private bool _deleteDialogIsOpen;

        List<ContentDetails> _contents
        {
            get
            {
                if (Service != null)
                {
                    var c = Service.GetContext().ContentDetails
                    .Where(x => x.Status == "Submitted/Pending") 
                    .Include(x => x.CreatedBy)
                    .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                    .Include(x=>x.ModerationComments)
                    .OrderBy(x => x.Id)
                    .ToList();
                    return c;
                }
                else
                {
                    return new();
                }
            }
        }

        List<ContentDetails> _approvedContents
        {
            get
            {
                if (Service != null)
                {
                    var c = Service.GetContext().ContentDetails
                    .Where(x => x.Status == "Approved")
                    .Include(x => x.CreatedBy)
                    .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                    .Include(x => x.ModerationComments)
                    .OrderBy(x => x.Id)
                    .ToList();
                    return c;
                }
                else
                {
                    return new();
                }
            }
        }

        List<ContentDetails> _rejectedContents
        {
            get
            {
                if (Service != null)
                {
                    var c = Service.GetContext().ContentDetails
                    .Where(x => x.Status == "Rejected")
                    .Include(x => x.CreatedBy)
                    .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                    .Include(x => x.ModerationComments)
                    .OrderBy(x => x.Id)
                    .ToList();
                    return c;
                }
                else
                {
                    return new();
                }
            }
        }
        List<ContentDetails> _archivedContents
        {
            get
            {
                string[] notList = new[] { "Submitted/Pending", "Rejected", "Approved" };
                if (Service != null)
                {
                    var c = Service.GetContext().ContentDetails
                    .Where(x => !notList.Contains( x.Status))
                    .Include(x => x.CreatedBy)
                    .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                    .Include(x => x.ModerationComments)
                    .OrderBy(x => x.Id)
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
        
    }
}

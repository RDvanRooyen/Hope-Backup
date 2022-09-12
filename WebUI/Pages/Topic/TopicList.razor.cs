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

namespace WebUI.Pages.Topic
{
    public partial class TopicList
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        ServiceBase Service { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
        }

        public List<WebUI.Data.Models.Topic> _topics
        {
            get
            {
                return Service.GetContext().Topics.ToList();
            }
        }

        private void DeleteItem(WebUI.Data.Models.Topic t)
        {
            Service.GetContext().Remove(t);
            Service.SaveChanges();
        }
        private void ApproveItem(WebUI.Data.Models.Topic t)
        {
            t.Approved = true;
            Service.SaveChanges();
        }
    }
}

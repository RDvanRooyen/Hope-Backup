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

    public partial class SearchContent
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [CascadingParameter(Name = "MainLayout")]
        public MainLayout MainLayout { get; set; }
        public ServiceBase Service { get; private set; } 

        private string userId;

        private string SearchKeyword { get; set; }
        private int? SelectedSubject { get; set; }
        private int? SelectedGrade { get; set; }
        private List<Data.Models.Subject> _subjects;
        private List<Data.Models.Grade> _grades; List<ContentDetails> _featuredList = new();
        List<ContentDetails> _newContents = new();
        private string SelectedSortBy = "Featured";
        bool ShowAsGrid = true;

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
            this.userId = Service.GetCurrentUser().Id;
            _subjects = Service.GetContext().Subjects.OrderBy(x => x.Id).ToList();
            _grades = Service.GetContext().Grades.OrderBy(x => x.Id).ToList();

        }
        protected override async Task OnParametersSetAsync()
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
                    .Where(x => x.Status == "Approved")
                    .OrderByDescending(x => x.ChangedDateTime)
                    .ThenByDescending(x => x.CreatedDateTime)
                    .Take(4)
                    .ToList();
            MainLayout.HideProgressBar();
        }
        public void SearchForContent()
        {
            NavManager.NavigateTo($"/content/list/result?k={SearchKeyword}&s={SelectedSubject}&g={SelectedGrade}&sort={SelectedSortBy}&grid={(ShowAsGrid ? "1" : "0")}", forceLoad: true);
        }


    }
}
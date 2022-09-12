using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.Models;

namespace WebUI.Components
{
    public partial class ContentDetailsThumbnailSearchComponent
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        private ContentFiles photo;
        private string _cssClass;

        [Parameter]
        public ContentDetails ContentDetails { get; set; }

        [Parameter]
        public string PhotoCardCSSClass { get; set; }

        [Parameter]
        public bool IsGrid { get; set; }
        protected override async Task OnInitializedAsync()
        {
            this.photo = ContentDetails.ContentFiles.Where(x => x.Category == ContentFiles.PhotoContentFileCategory && x.IsPrimaryPhoto).FirstOrDefault();
            if (photo == null)
            {
                photo = ContentDetails.ContentFiles.Where(x => x.Category == ContentFiles.PhotoContentFileCategory).OrderBy(x => x.Id).FirstOrDefault();
            }
        }
        protected override void OnParametersSet()
        {
            this._cssClass = PhotoCardCSSClass ?? "default-photo";
        }
        private void ShowContentDetail(int contentDetailId)
        {
            NavManager.NavigateTo("/content/view/" + contentDetailId, forceLoad: true);
        }
    }
}

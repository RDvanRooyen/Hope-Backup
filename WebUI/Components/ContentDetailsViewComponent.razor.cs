using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.Models;
using WebUI.Services;
namespace WebUI.Components
{
    public partial class ContentDetailsViewComponent
    {
        private ContentDetails _contentDetails;
        private bool _isBookmarked;
        private decimal _givenRating = -1;
        private string _newComment = string.Empty;
        private string _newQuestion = string.Empty;

        [Inject]
        public IConfiguration Configuration { get; set; }
        [Parameter]
        public int? ContentId { get; set; }
        [Parameter]
        public bool HideSideBar { get; set; }
        public ServiceBase Service { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);

        }

        protected override async Task OnParametersSetAsync()
        {
            if (ContentId != null)
            {
                var userId = Service.GetCurrentUser().Id;
                _contentDetails = await Service.GetContext().ContentDetails
                   .Include(x => x.CreatedBy)
                   .Include(x => x.ContentExternalLinks)
                   .Include(x => x.ContentFiles)
                   .Include(x => x.ContentGrades).ThenInclude(x => x.Grade)
                   .Include(x => x.ContentStandards).ThenInclude(x => x.Standard)
                   .Include(x => x.ContentSubjects).ThenInclude(x => x.Subject)
                   .Include(x => x.Derivations).ThenInclude(x => x.ContentDetails)
                   .Include(x => x.ContentTopics).ThenInclude(x => x.Topic)
                   .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                   .Where(x => x.Id == ContentId).FirstOrDefaultAsync();

                _isBookmarked = Service.GetContext().ContentBookmarks.Where(x => x.UserId == userId && x.ContentDetailsId == ContentId).Any();
                _givenRating = Service.GetContext().ContentRatings.Where(x => x.UserId == userId && x.ContentDetailsId == ContentId).Select(x => x.Rating).FirstOrDefault() ?? -1;

            }
            else
            {
                //NavManager.NavigateTo("/content/list/new");
            }
        }

        private List<ContentComment> _topComments
        {
            get
            {
                return Service.GetContext().ContentComments.Include(x => x.User).Where(x=>x.ContentDetailsId == ContentId && x.Approved).OrderByDescending(x => x.Added).Take(10).ToList();
            }
        }

        private string GetDownloadLink(string baseDir, string filepath)
        {
            baseDir = baseDir.Replace("wwwroot", string.Empty);
            return Path.Combine(baseDir, filepath);
        }

        private void ToggleBookmark()
        {
            var userId = Service.GetCurrentUser().Id;
            var existed = Service.GetContext().ContentBookmarks.Where(x => x.UserId == userId && x.ContentDetailsId == ContentId).FirstOrDefault();
            if (existed == null)
            {
                existed = new ContentBookmark
                {
                    UserId = userId,
                    ContentDetailsId = ContentId.Value
                };
                Service.GetContext().ContentBookmarks.Add(existed);
                _isBookmarked = true;
                _toaster.Add("Bookmark added!", MatToastType.Success);
            }
            else
            {
                Service.GetContext().ContentBookmarks.Remove(existed);
                _isBookmarked = false;
                _toaster.Add("Bookmark removed!", MatToastType.Success);
            }
            Service.SaveChanges();
        }

        private void RateThis(int rating)
        {
            _givenRating = rating;
            var userId = Service.GetCurrentUser().Id;
            var existed = Service.GetContext().ContentRatings.Where(x => x.UserId == userId && x.ContentDetailsId == ContentId).FirstOrDefault();
            if (existed == null)
            {
                existed = new ContentRating
                {
                    UserId = userId,
                    ContentDetailsId = ContentId.Value,
                    Rating = rating,
                    Added = DateTime.Now
                };
                Service.GetContext().ContentRatings.Add(existed);
                _toaster.Add("Rating added!", MatToastType.Success);
            }
            else
            {
                existed.Rating = rating;
                existed.Added = DateTime.Now;
                _toaster.Add("Rating updated!", MatToastType.Success);
            }
            Service.SaveChanges();

            var average = Service.GetContext().ContentRatings.Where(x => x.ContentDetailsId == ContentId).Average(x => x.Rating);
            _contentDetails.AverageRating = average;
            Service.SaveChanges();
        }

        private void AddComment()
        {
            var userId = Service.GetCurrentUser().Id;
            var comment = new ContentComment
            {
                Added = DateTime.Now,
                UserId = userId,
                ContentDetailsId = ContentId.Value,
                Comment = _newComment,

            };
            Service.GetContext().ContentComments.Add(comment);
            Service.SaveChanges();
            StateHasChanged();
            _toaster.Add("Your comment has been submited!", MatToastType.Success);
            _newComment = string.Empty;
        }
        private void AddQuestion()
        {
            var userId = Service.GetCurrentUser().Id;
            var q = new ContentQuestion
            {
                Added = DateTime.Now,
                UserId = userId,
                ContentDetailsId = ContentId.Value,
                Question = _newQuestion,

            };
            Service.GetContext().ContentQuestions.Add(q);
            Service.SaveChanges();
            StateHasChanged();
            _toaster.Add("Your question has been added!", MatToastType.Success);
            _newQuestion = string.Empty;
        }



        private void CreateDerivation()
        {
            NavManager.NavigateTo("/content/entry/0?d=" + ContentId.Value);
        }
    }
}

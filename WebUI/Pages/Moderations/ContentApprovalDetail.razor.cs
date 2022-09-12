using Blazored.TextEditor;
using MatBlazor;
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
namespace WebUI.Pages.Moderations
{
    public partial class ContentApprovalDetail
    {
        [Parameter]
        public int? ContentDetailId { get; set; }


        [Inject]
        public IConfiguration Configuration { get; set; }

        private string _selectedPrimaryPhoto;


        List<ContentDerivationOption> _derivations = new List<ContentDerivationOption>();

        string _openEducationLicensingAddressed = "Yes";


        public int Step { get; set; } = 1;

        ServiceBase Service { get; set; }

        private ApplicationUser _currentUser;
        ContentDetails _contentDetails = null;
        string _rejectReason;
        bool _showReject;
        bool _showDeleteConfirm;

        //public Dictionary<string, TempFile> PhotoDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> MaterialDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> ResourceDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> CommunityDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> FormativeDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> SummativeDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> PacingDictionary = new Dictionary<string, TempFile>();
        //public Dictionary<string, TempFile> StudentWorkDictionary = new Dictionary<string, TempFile>(); 
        private List<CoAuthorItem> _coAuthorEmails = new List<CoAuthorItem>();

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
            this._currentUser = Service.GetCurrentUser();
            _topics = Service.GetContext().Topics.ToList();
            if ((ContentDetailId ?? 0) == 0)
            {
                NavManager.NavigateTo("/moderation/contents");
            }
            else
            {
                _contentDetails = Service.GetContext().ContentDetails
                    .Include(x => x.CreatedBy)
                    .Include(x => x.ContentExternalLinks)
                    .Include(x => x.Derivations).ThenInclude(x => x.ContentDetails)
                    .Include(x => x.ContentFiles)
                    .Include(x => x.ContentGrades).ThenInclude(x => x.Grade)
                    .Include(x => x.ContentStandards).ThenInclude(x => x.Standard)
                    .Include(x => x.ContentSubjects).ThenInclude(x => x.Subject)
                    .Include(x => x.ContentTopics).ThenInclude(x => x.Topic)
                    .Include(x => x.CoAuthors).ThenInclude(x => x.Author)
                    .Where(x => x.Id == ContentDetailId).FirstOrDefault();

                SelectedTopics = _contentDetails.ContentTopics.Select(x => x.Topic).ToList();

            }
        }


        private bool _isSaving;
        private string _commentForSubmitter;
        private string _commentInternal;

        private List<ModerationComment> CommentsForSubmitter
        {
            get
            {
                return Service.GetContext().ModerationComments
                    .Where(x => x.ContentDetailsId == ContentDetailId && x.IsInternalComment == false && x.Step == Step)
                    .OrderByDescending(x => x.CommentDate)
                    .ToList();
            }
        }
        private List<ModerationComment> InternalComments
        {
            get
            {
                return Service.GetContext().ModerationComments
                    .Where(x => x.ContentDetailsId == ContentDetailId && x.IsInternalComment == true && x.Step == Step)
                    .OrderByDescending(x => x.CommentDate)
                    .ToList();
            }
        }

        private void PerformStep(int step)
        {
            Step = step;
            StateHasChanged();

        }
        private void PerformStepPrevious()
        {
            if (Step > 1)
            {
                Step--;
            }
        }
        private void PerformStepNext()
        {
            if (Step < 5)
            {
                Step++;
            }
        }




        private void AddCommentForSubmiter()
        {
            var c = new ModerationComment
            {
                CommentDate = DateTime.Now,
                IsInternalComment = false,
                Moderator = _currentUser,
                ContentDetailsId = ContentDetailId,
                Comment = _commentForSubmitter,
                Step = Step,
            };
            Service.GetContext().ModerationComments.Add(c);
            Service.SaveChanges();
            _toaster.Add("Comment added", MatToastType.Success);
            _commentForSubmitter = string.Empty;
        }

        public void AddInternalComment()
        {
            var c = new ModerationComment
            {
                CommentDate = DateTime.Now,
                IsInternalComment = true,
                Moderator = _currentUser,
                ContentDetailsId = ContentDetailId,
                Comment = _commentInternal,
                Step = Step,

            };
            Service.GetContext().ModerationComments.Add(c);
            Service.SaveChanges();
            _toaster.Add("Comment added", MatToastType.Success);
            _commentInternal = string.Empty;
        }

        private string GetDownloadLink(string baseDir, string filepath)
        {
            baseDir = baseDir.Replace("wwwroot", string.Empty);
            return Path.Combine(baseDir, filepath);
        }

        ModerationComment _deleteItem;
        private void DeleteCommment(ModerationComment c)
        {
            _deleteItem = c;
            _showDeleteConfirm = true;
        }

        private void ConfirmDeleteComment()
        {
            Service.GetContext().ModerationComments.Remove(_deleteItem);
            Service.SaveChanges();
            _toaster.Add("Comment deleted", MatToastType.Success);
        }

        private void SubmitReject()
        {
            _isSaving = true;
            _contentDetails.RejectionReason = _rejectReason;
            _contentDetails.RejectionDateTime = DateTime.Now;
            _contentDetails.RejectedById = _currentUser.Id;
            _contentDetails.ApprovedById = null;
            _contentDetails.ApprovedDateTime = null;
            _contentDetails.Status = "Rejected";
            _rejectReason = string.Empty;
            Service.SaveChanges();
            _toaster.Add("Content Rejected", MatToastType.Success);
            _isSaving = false;
        }

        private void Reject()
        {
            _showReject = true;
        }

        private void Approve()
        {
            _isSaving = true;

            _contentDetails.ContentTopics = GetSelectedTopics();
            _contentDetails.RejectionReason = string.Empty;
            _contentDetails.RejectionDateTime = null;
            _contentDetails.RejectedById = null;
            _contentDetails.ApprovedById = _currentUser.Id;
            _contentDetails.ApprovedDateTime = DateTime.Now;
            _contentDetails.Status = "Approved";
            _rejectReason = string.Empty;
            Service.SaveChanges();
            _toaster.Add("Content Approved", MatToastType.Success);
            _isSaving = false;
            Step = 6;
        }
        private IList<WebUI.Data.Models.Topic> SelectedTopics = new List<WebUI.Data.Models.Topic>();

        private List<Data.Models.Topic> _topics;
        private async Task<IEnumerable<WebUI.Data.Models.Topic>> SearchTopic(string searchText)
        {
            searchText = searchText?.ToLower();
            var search = _topics.Where(x => x.Title != null && x.Title.ToLower().Contains(searchText)).ToList();
            return search;
        }
        private List<ContentTopic> GetSelectedTopics()
        {
            var selected = SelectedTopics.Where(x => x.Id > 0)
                .Select(x => new ContentTopic { ContentDetailsId = _contentDetails.Id, TopicId = x.Id, Topic = x }).ToList();
            var newTopics = SelectedTopics.Where(x => x.Id == 0);
            foreach (var nt in newTopics)
            {
                selected.Add(new ContentTopic
                {
                    Topic = nt,
                });
            }
            return selected;

        }
    }

}


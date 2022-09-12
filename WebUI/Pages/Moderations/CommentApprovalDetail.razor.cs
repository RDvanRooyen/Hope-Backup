using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.Models;
using WebUI.Services;

namespace WebUI.Pages.Moderations
{
    public partial class CommentApprovalDetail
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [Parameter]
        public int? CommentId { get; set; }
        public int? ContentId { get; set; }

        public ContentComment Comment { get; set; }
        public ServiceBase Service { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
        }
        protected override void OnParametersSet()
        {
            Comment = Service.GetContext().ContentComments.Include(x=>x.User).Where(x => x.Id == CommentId).FirstOrDefault();
            ContentId = Comment.ContentDetailsId;
        }

        public void CancelComment()
        {
            NavManager.NavigateTo("/moderation/comments");
        }
        public void RejectComment()
        {
            var uid = Service.GetCurrentUser().Id;
            var c = Comment;
            c.Approved = false;
            c.ApprovedDate = DateTime.Now;
            c.ModeratorId = uid;
            Service.SaveChanges();
            NavManager.NavigateTo("/moderation/comments");

        }
        public void ApproveComment()
        {
            var uid = Service.GetCurrentUser().Id;
            var c = Comment;
            c.Approved = true;
            c.ApprovedDate = DateTime.Now;
            c.ModeratorId = uid;
            Service.SaveChanges();
            NavManager.NavigateTo("/moderation/comments");
        }
    }
}

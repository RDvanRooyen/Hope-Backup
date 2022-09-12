using System;

namespace WebUI.Data.Models
{
    public partial class ContentComment
    {
        public int Id { get; set; }
        public int? ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Comment { get; set; }
        public DateTime Added { get; set; }
        public bool Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ModeratorId { get; set; }
        public ApplicationUser Moderator { get; set; }
    }
}

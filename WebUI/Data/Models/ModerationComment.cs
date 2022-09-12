using System;

namespace WebUI.Data.Models
{
    public partial class ModerationComment
    {
        public int Id { get; set; }
        public int? ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public ApplicationUser Moderator { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsInternalComment { get; set; }
        public string Comment { get; set; }
        public int Step { get; set; }

    }
}

namespace WebUI.Data.Models
{
    public partial class ContentBookmark
    {
        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool Shared { get; set; }
    }
}

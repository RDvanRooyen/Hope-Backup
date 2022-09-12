namespace WebUI.Data.Models
{
    public partial class ContentExternalLink
    {
        public int Id { get; set; }

        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public string ExternalLink { get; set; }
    }
}

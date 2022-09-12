using WebUI.Data.Models;

namespace WebUI.Data.ViewModels
{
    public class ContentDerivationOption
    {
        public string Category { get; set; }
        public int? ContentId { get; set; }
        public string ExternalLink { get; set; }

        public ContentDetails ContentDetails { get; set; }
    }
}

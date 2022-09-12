namespace WebUI.Data.Models
{
    public partial class ContentStandard
    {
        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public int StandardId { get; set; }
        public Standard Standard { get; set; }
    }
}
 
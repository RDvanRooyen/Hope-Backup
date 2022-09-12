namespace WebUI.Data.Models
{
    public partial class ContentSubject
    {
        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}

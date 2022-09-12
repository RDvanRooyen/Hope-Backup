namespace WebUI.Data.Models
{
    public partial class ContentGrade
    {
        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}

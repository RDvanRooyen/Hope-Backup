namespace WebUI.Data.Models
{
    public partial class UserGrade
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}

namespace WebUI.Data.Models
{
    public partial class UserSubject
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}

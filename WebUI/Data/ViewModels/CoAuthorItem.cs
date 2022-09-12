namespace WebUI.Data.ViewModels
{
    public class CoAuthorItem
    {
        public string EmailAddress { get; set; }
        public bool IsValid { get; set; }
        public string UserId { get; set; }
        public string InvalidMessage { get; internal set; }
        public ApplicationUser Author { get; internal set; }
    }
}

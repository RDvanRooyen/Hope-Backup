namespace WebUI.Data.Models
{
    public partial class ContentAuthor
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }        

        public ContentAuthor(string authorId, int contentId)
        {
            ContentId = contentId;       
            AuthorId = authorId;            
        }
        public ContentAuthor()
        {

        }
    }
}
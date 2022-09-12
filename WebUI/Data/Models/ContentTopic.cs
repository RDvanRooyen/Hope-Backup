namespace WebUI.Data.Models
{
    public partial class ContentTopic
    {
        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}

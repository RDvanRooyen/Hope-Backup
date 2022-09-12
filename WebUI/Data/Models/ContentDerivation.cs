namespace WebUI.Data.Models
{
    public partial class ContentDerivation
    {
        public int ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public int? DerivationId { get; set; }
        public ContentDetails Derivation { get; set; }
    }

}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Data.Models
{
    public partial class ContentRating
    {
        public int Id { get; set; }
        public int? ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Rating { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Added { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.Models
{
    public partial class ContentQuestion
    {
        public int Id { get; set; }
        public int? ContentDetailsId { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Question { get; set; }
        public DateTime Added { get; set; } 
    }
}

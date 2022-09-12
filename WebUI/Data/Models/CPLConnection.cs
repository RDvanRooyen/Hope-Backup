using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Data.Models
{
    public partial class CPLConnection
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Culture/Place/LocalConnection is required.")]
        [StringLength(50, ErrorMessage = "Culture/Place/LocalConnection is too long.")]
        public string Title { get; set; }
        public string LatinVersion { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime? ChangedDateTime { get; set; }
        public string ChangedById { get; set; }
        public ApplicationUser ChangedBy { get; set; }

        public bool Deleted { get; set; }
        public string DeletedById { get; set; }
        public ApplicationUser DeletedBy { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}

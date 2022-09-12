using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebUI.Data.Enums;

namespace WebUI.Data.Models
{
    public partial class ContentDetails
    {
        [Key]
        public int Id { get; set; }
          
        public string Title { get; set; }
         
        public string Summary { get; set; }
        public string SummaryText { get; set; }

        public bool OpenEduLicAdd { get; set; }

        [NotMapped]
        public string OpenEduLicAddString
        {
            get
            {
                return OpenEduLicAdd ? "Yes" : "No";
            }
            set
            {
                OpenEduLicAdd = value != null && value == "Yes";
            }
        }

        
       
        public string EssentialQuestion { get; set; }

       
        public string Objective { get; set; }

        
        public string ConnectionToHawaii { get; set; }
        public string PlaceName { get; set; }

        public string Duration { get; set; }
        public DateTime? RejectionDateTime { get;  set; }
        public string RejectionReason { get; set; }


        public string MaterialsText { get; set; }
        public string MaterialFiles { get; set; }

        public string ResourcesText { get; set; } 

        public string ConnectionText { get; set; } 


  

        public string FormativeAssessments { get; set; }
         

        public string SummativeAssessments { get; set; }
         

        public string FinalProduct { get; set; } 
         

        public string Status { get; set; } = "Editing";
        public bool Featured { get; set; } = false;
        public DateTime? FeaturedDateTime { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? AverageRating { get; set; }

        public bool AllowUserEmail { get; set; } = false;


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


        public DateTime? SubmittedDateTime { get; set; }
        public DateTime? ApprovedDateTime { get; set; }

        /// these fields are to speed up searching process
        public string AuthorText { get; set; }
        public string GradesText { get; set; }
        public string SubjectsText { get; set; }
        public string TopicsText { get; set; }
        public string CoAuthorsText { get; set; }
        public string PrimaryPhotoPath { get; set; }
        public string GradesIdText { get;  set; }
        public string SubjectsIdText { get;  set; }
        public string TopicsIdText { get;  set; }
        public string RejectedById { get;  set; }
        public string ApprovedById { get;  set; }
    }
}

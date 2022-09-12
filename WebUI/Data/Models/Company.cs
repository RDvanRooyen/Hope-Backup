using System.ComponentModel.DataAnnotations;

namespace WebUI.Data.Models
{
    public partial class Company
    {
        [Key]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(255, ErrorMessage = "Company Name is too long.")]
        public string CompanyName { get; set; }
        
        [ValidateComplexType]
        public Address CompanyAddress { get; set; } = new();

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(255, ErrorMessage = "Phone number is too long.")]
        public string CompanyPhoneNo { get; set; }

        [Required(ErrorMessage = "Report email is required.")]
        [StringLength(255, ErrorMessage = "Report email is too long.")]
        public string CompanyReportEmail { get; set; }

        [Required(ErrorMessage = "Sender email is required.")]
        [StringLength(255, ErrorMessage = "Sender email is too long.")]
        public string CompanySenderEmail { get; set; }

       // [Required(ErrorMessage = "Company Logo is required.")]
        public string CompanyLogoImage { get; set; }
        
       // [Required(ErrorMessage = "Favicon is required.")]
        public string CompanyFavicon { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebUI.Data.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Street address is required.")]
        [StringLength(255, ErrorMessage = "Street address is too long.")]
        public string CompanyAddressLine1 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(255, ErrorMessage = "City is too long.")]
        public string CompanyCity { get; set; }
        
        [Required(ErrorMessage = "State is required.")]
        [StringLength(255, ErrorMessage = "State is too long.")]
        public string CompanyState { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [StringLength(255, ErrorMessage = "Zip Code is too long.")]
        public string CompanyZipCode { get; set; }
    }
}

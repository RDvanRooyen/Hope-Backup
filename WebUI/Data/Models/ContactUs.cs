using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Data.Models
{
    public partial class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Responded { get; set; }
        public string Response { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime RespondDate { get; set; }
    }
}

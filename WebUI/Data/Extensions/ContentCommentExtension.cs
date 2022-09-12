using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.Models
{
    public partial class ContentComment
    {
        [NotMapped]
        public bool Checked { get; set; }
    }
}

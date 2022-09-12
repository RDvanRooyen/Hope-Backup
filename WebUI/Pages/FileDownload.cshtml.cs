using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WebUI.Data;

namespace WebUI.Pages
{
    public class FileDownloadModel : PageModel
    {
        private IConfiguration configuration;
        private readonly ApplicationDbContext<ApplicationUser> db; 

        public FileDownloadModel(ApplicationDbContext<ApplicationUser> db, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.db = db; 
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var contentFile = db.ContentFiles.Find(id);
            var stream = contentFile.GetFileStream(configuration["AppSettings:BaseDirectory"], contentFile.RelativePath);
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();

            return File(bytes, "application/force-download", contentFile.RelativePath.Split(@"\").Last());
        }
    }
}

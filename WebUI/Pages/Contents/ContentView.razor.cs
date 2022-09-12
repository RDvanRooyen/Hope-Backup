using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.Models;
using WebUI.Services;
namespace WebUI.Pages.Contents
{
    public partial class ContentView
    { 

        [Inject]
        public IConfiguration Configuration { get; set; }
        [Parameter]
        public int? ContentId { get; set; }
        
    }
}

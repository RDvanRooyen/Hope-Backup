using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data;

namespace WebUI.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public partial class IndexModel : PageModel
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IDictionary<ApplicationUser, IList<string>> UserRoleDict { get; set; }

        //[TempData]
        //public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserRoleDict = new Dictionary<ApplicationUser, IList<string>>();

            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                UserRoleDict.Add(user, await _userManager.GetRolesAsync(user));
            }

            return Page();
        }
    }
}
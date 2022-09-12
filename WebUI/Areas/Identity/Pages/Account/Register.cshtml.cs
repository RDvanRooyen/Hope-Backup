using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Identity.Pages.Admin;
using WebUI.Data;
using WebUI.Data.Models;
using WebUI.Data.Enums;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext<ApplicationUser> _dbContext;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext<ApplicationUser> dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<IdentityRole> Roles { get { return _roleManager.Roles.ToList(); } }
        public MultiSelectList GradesList { get; set; }
        public MultiSelectList SubjectsList { get; set; }
        [BindProperty]
        public int[] SelectedGrades { get; set; }
        [BindProperty]
        public int[] SelectedSubjects { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email*")]
            public string Email { get; set; }

            [Required]
            [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password*")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "First Name*")]
            [Required(ErrorMessage = "First Name is required.")]
            [StringLength(50, ErrorMessage = "First Name is too long.")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name*")]
            [Required(ErrorMessage = "Last Name is required.")]
            [StringLength(50, ErrorMessage = "Last Name is too long.")]
            public string LastName { get; set; }

            [Display(Name = "School*")]
            [Required(ErrorMessage = "School is required.")]
            [StringLength(50, ErrorMessage = "School is too long.")]
            public string School { get; set; }

            [Display(Name = "Status")]
            [Required(ErrorMessage = "Status is required.")]
            public string Status { get; set; }
            [Required]
            [Display(Name = "Role")]
            public string RoleName { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var grades = await _dbContext.Grades.ToListAsync();
            GradesList = new MultiSelectList(grades, nameof(Grade.Id), nameof(Grade.Title));
            var subjects = await _dbContext.Subjects.ToListAsync();
            SubjectsList = new MultiSelectList(subjects, nameof(Subject.Id), nameof(Subject.Title));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    School = Input.School,
                    Status = Input.Status,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // add new user to selected Role
                    await _userManager.AddToRoleAsync(user, Input.RoleName);

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    foreach (var gradeId in SelectedGrades.ToList())
                    {
                        var grade = _dbContext.Grades.SingleOrDefault(x => x.Id == gradeId);
                        var exists = _dbContext.UserGrades.Any(x => x.GradeId == gradeId && x.UserId == user.Id);

                        _dbContext.UserGrades.Add(new UserGrade() { GradeId = gradeId, UserId = user.Id });
                    }

                    foreach (var subjectId in SelectedSubjects.ToList())
                    {
                        var subject = _dbContext.Subjects.SingleOrDefault(x => x.Id == subjectId);
                        var exists = _dbContext.UserSubjects.Any(x => x.SubjectId == subjectId && x.UserId == user.Id);

                        _dbContext.UserSubjects.Add(new UserSubject() { SubjectId = subjectId, UserId = user.Id });
                    }
                    _dbContext.SaveChanges();

                    if (!user.EmailConfirmed)
                    {
                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    }

                    return LocalRedirect(returnUrl);

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data;
using WebUI.Data.Models;

namespace WebUI.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<EditModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext<ApplicationUser> _dbContext;

        public EditModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<EditModel> logger,
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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public string Status { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Role { get; set; }
        public IList<IdentityRole> RoleList { get; set; }
        public MultiSelectList GradesList { get; set; }
        public MultiSelectList SubjectsList { get; set; }
        [BindProperty]
        public int[] SelectedGrades { get; set; }
        [BindProperty]
        public int[] SelectedSubjects { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string FirstNameStatusMessage { get; set; }
        [TempData]
        public string LastNameStatusMessage { get; set; }
        [TempData]
        public string GradeIdStatusMessage { get; set; }
        [TempData]
        public string SubjectIdStatusMessage { get; set; }
        [TempData]
        public string SchoolStatusMessage { get; set; }
        [TempData]
        public string StatusStatusMessage { get; set; }
        [TempData]
        public string PasswordStatusMessage { get; set; }
        [TempData]
        public string RoleStatusMessage { get; set; }        
        [TempData]
        public string EmailStatusMessage { get; set; }

        public class InputModel
        {
            // profile section
            [Display(Name = "First Name")]
            [StringLength(50, ErrorMessage = "First Name is too long.")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [StringLength(50, ErrorMessage = "Last Name is too long.")]
            public string LastName { get; set; }           

            [Display(Name = "School")]
            [StringLength(50, ErrorMessage = "School is too long.")]
            public string School { get; set; }
            

            [Display(Name = "Status")]
            public string Status { get; set; }

            // password section
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // email section
            [EmailAddress]
            [Display(Name = "Current Email")]
            public string CurrentEmail { get; set; }

            // email section
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }

            [Display(Name = "Role")]
            public string NewRole { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userGrades = _dbContext.UserGrades.Where(g => g.UserId == user.Id);
            var userSubjects = _dbContext.UserSubjects.Where(g => g.UserId == user.Id);
            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                School = user.School,
                Status = user.Status,
            };
            SelectedGrades = userGrades.Select(x => x.GradeId).ToArray();
            SelectedSubjects = userSubjects.Select(x => x.SubjectId).ToArray();
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var grades = await _dbContext.Grades.ToListAsync();
            GradesList = new MultiSelectList(grades, nameof(Grade.Id), nameof(Grade.Title));
            var subjects = await _dbContext.Subjects.ToListAsync();
            SubjectsList = new MultiSelectList(subjects, nameof(Subject.Id), nameof(Subject.Title));

            var user = _userManager.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                School = user.School;
                Status = user.Status;
                Email = user.Email;
                EmailConfirmed = user.EmailConfirmed;
                var userRoles = await _userManager.GetRolesAsync(user);
                Role = userRoles.FirstOrDefault();
                RoleList = _roleManager.Roles.ToList();

                await LoadAsync(user);
                return Page();
            }
            else
            {
                return NotFound($"Unable to load user with Id '{userId}'.");
            }
        }

        public async Task<IActionResult> OnPostAsync(string userId)
        {
            var user = _userManager.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (user == null)
            {
                return NotFound($"Unable to load user'{Input.CurrentEmail}'.");
            }
            else
            {
                // in case the page needs to be rendered again due to errors (e.g. wrong password)

                //var userGrades = _dbContext.UserGrades.Where(g => g.UserId == user.Id);
                //var userSubjects = _dbContext.UserSubjects.Where(g => g.UserId == user.Id);

                //SelectedGrades = userGrades.Select(x => x.GradeId).ToArray();
                //SelectedSubjects = userSubjects.Select(x => x.SubjectId).ToArray();

                FirstName = user.FirstName;
                LastName = user.LastName;
                School = user.School;
                Status = user.Status;
                Email = user.Email;
                EmailConfirmed = user.EmailConfirmed;
                var userRoles = await _userManager.GetRolesAsync(user);
                Role = userRoles.FirstOrDefault();
                RoleList = _roleManager.Roles.ToList();
            }

            // update FirstName if the FirstName form has been Changed
            if (!string.IsNullOrEmpty(Input.FirstName))
            {
                if (Input.FirstName != user.FirstName)
                {
                    user.FirstName = Input.FirstName;                    
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation("Admin changed a users First Name successfully.");
                    FirstNameStatusMessage = "First Name Updated Successfully.";                    
                }
            }

            // update LastName if the LastName form has been Changed
            if (!string.IsNullOrEmpty(Input.LastName))
            {
                if (Input.LastName != user.LastName)
                {
                    user.LastName = Input.LastName;
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation("Admin changed a users First Name successfully.");
                    LastNameStatusMessage = "First Name Updated Successfully.";
                }
            }

            // update Grades if the Grades form has been Changed
            bool gradesChanged = false;
            var removeOldSelectedGrades = _dbContext.UserGrades.Where(g => g.UserId == user.Id).ToArray();
            int[] gradeOld = Array.ConvertAll(removeOldSelectedGrades, s => s.GradeId);
            int[] gradeNew = SelectedGrades;
            if (gradeOld.Length == gradeNew.Length)
            {
                for (int i = 0; i < gradeNew.Length; i++)
                {
                    if (gradeNew[i] != gradeOld[i])
                    {

                        gradesChanged = true;
                    }
                }
            }
            else
            {
                gradesChanged = true;
            }

            if (gradesChanged)
            {
                if (user.Grades == null)
                {
                    user.Grades = new List<UserGrade>();
                }
                    foreach (var oldGrade in removeOldSelectedGrades)
                    {
                        _dbContext.UserGrades.Remove(oldGrade);
                    }                                
                foreach (var gradeId in SelectedGrades.ToList())
                {
                    var grade = _dbContext.Grades.SingleOrDefault(x => x.Id == gradeId);
                    var exists =_dbContext.UserGrades.Any(x => x.GradeId == gradeId && x.UserId == user.Id);
                    
                    _dbContext.UserGrades.Add(new UserGrade() {GradeId = gradeId,UserId = user.Id });
                }
                _dbContext.SaveChanges();
                //await _userManager.UpdateAsync(user);
                _logger.LogInformation("Admin changed a users Grades successfully.");
                GradeIdStatusMessage = "Grades Updated Successfully.";
            }

            // update Subjects if the Subjects form has been Changed
            bool subjectsChanged = false;
            var removeOldSelectedSubjects = _dbContext.UserSubjects.Where(g => g.UserId == user.Id).ToArray();
            int[] subjectOld = Array.ConvertAll(removeOldSelectedSubjects, s => s.SubjectId);
            int[] subjectNew = SelectedSubjects;
            if (subjectOld.Length == subjectNew.Length)
            {
                for (int i = 0; i < subjectNew.Length; i++)
                {
                    if (subjectNew[i] != subjectOld[i])
                    {

                        subjectsChanged = true;
                    }
                }
            }
            else
            {

                subjectsChanged = true;
            }

            if (subjectsChanged)
            {
                if (user.Subjects == null)
                {
                    user.Subjects = new List<UserSubject>();
                }

                var removeOldSelected = _dbContext.UserSubjects.Where(g => g.UserId == user.Id).ToArray();

                foreach (var oldSubject in removeOldSelected)
                {
                    _dbContext.UserSubjects.Remove(oldSubject);
                }

                foreach (var subjectId in SelectedSubjects.ToList())
                {
                    var subject = _dbContext.Subjects.SingleOrDefault(x => x.Id == subjectId);
                    var exists = _dbContext.UserSubjects.Any(x => x.SubjectId == subjectId && x.UserId == user.Id);

                    _dbContext.UserSubjects.Add(new UserSubject() { SubjectId = subjectId, UserId = user.Id });
                }
                _dbContext.SaveChanges();
                //await _userManager.UpdateAsync(user);
                _logger.LogInformation("Admin changed a users Subjects successfully.");
                SubjectIdStatusMessage = "Subjects Updated Successfully.";
            }

            // update School if the School form has been Changed
            if (!string.IsNullOrEmpty(Input.School))
            {
                if (Input.School != user.School)
                {
                    user.School = Input.School;
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation("Admin changed a users School successfully.");
                    SchoolStatusMessage = "School Updated Successfully.";
                }
            }

            // update Status if the Status form has been Changed
            if (!string.IsNullOrEmpty(Input.Status))
            {
                if (Input.Status != user.Status)
                {
                    user.Status = Input.Status;
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation("Admin changed a users Status successfully.");
                    StatusStatusMessage = "Status Updated Successfully.";
                }
            }

            //PasswordStatusMessage = "Password has not been changed.";
            //EmailStatusMessage = "Email has not been changed.";

            // update password if the password form has been submitted
            if (Input.NewPassword != null && Input.OldPassword != null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    var grades = await _dbContext.Grades.ToListAsync();
                    GradesList = new MultiSelectList(grades, nameof(Grade.Id), nameof(Grade.Title));

                    var subjects = await _dbContext.Subjects.ToListAsync();
                    SubjectsList = new MultiSelectList(subjects, nameof(Subject.Id), nameof(Subject.Title));

                    var userGrades = _dbContext.UserGrades.Where(g => g.UserId == user.Id);
                    var userSubjects = _dbContext.UserSubjects.Where(g => g.UserId == user.Id);

                    SelectedGrades = userGrades.Select(x => x.GradeId).ToArray();
                    SelectedSubjects = userSubjects.Select(x => x.SubjectId).ToArray();

                    //PasswordStatusMessage = "Password Updated Failed.";

                    await LoadAsync(user);
                    return Page();
                }
                else
                {
                    _logger.LogInformation("Admin changed a users password successfully.");
                    PasswordStatusMessage = "Password Updated Successfully.";
                }
            }

            // update email if the email field has been submitted
            if (!string.IsNullOrEmpty(Input.NewEmail))
            {
                if (Input.NewEmail != user.Email)
                {
                    user.Email = Input.NewEmail;
                    user.UserName = Input.NewEmail;
                    user.EmailConfirmed = true;
                    //await _userManager.SetEmailAsync(user, Input.NewEmail);
                    //await _userManager.SetUserNameAsync(user, Input.NewEmail);
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation("Admin changed a users email successfully.");
                    EmailStatusMessage = "Email Updated Successfully.";

                    //var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmailChange",
                    //    pageHandler: null,
                    //    values: new { userId = userId, email = Input.NewEmail, code = code },
                    //    protocol: Request.Scheme);
                    //await _emailSender.SendEmailAsync(
                    //    Input.NewEmail,
                    //    "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //EmailStatusMessage = "Confirmation link to change email sent to user";
                }
            }

            // get current role, and update user role if changed
            var oldRoles = await _userManager.GetRolesAsync(user);
            string oldRole = oldRoles.FirstOrDefault();
            if (Input.NewRole != oldRole)
            {
                // if new role is not empty, proceed to remove and add user to roles
                if (!string.IsNullOrWhiteSpace(Input.NewRole))
                {
                    // if old role is not empty, remove user from role
                    if (!string.IsNullOrWhiteSpace(oldRole))
                    {
                        await _userManager.RemoveFromRoleAsync(user, oldRole);
                    }

                    await _userManager.AddToRoleAsync(user, Input.NewRole);
                    _logger.LogInformation("Admin changed a users role successfully.");
                    RoleStatusMessage = "Role Updated Successfully.";
                }
            }
            return RedirectToPage();
        }
    }
}

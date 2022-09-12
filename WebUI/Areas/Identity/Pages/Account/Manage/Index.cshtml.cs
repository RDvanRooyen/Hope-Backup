
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebUI.Data;
using WebUI.Data.Models;

namespace HOPE_CleanBuild.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext<ApplicationUser> _dbContext;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IndexModel> logger,
            ApplicationDbContext<ApplicationUser> dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        public string Username { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public MultiSelectList GradesList { get; set; }
        public MultiSelectList SubjectsList { get; set; }
        [BindProperty]
        public int[] SelectedGrades { get; set; }
        [BindProperty]
        public int[] SelectedSubjects { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            [StringLength(50, ErrorMessage = "First Name is too long.")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [StringLength(50, ErrorMessage = "Last Name is too long.")]
            public string LastName { get; set; }

            [Display(Name = "School")]
            [StringLength(50, ErrorMessage = "School is too long.")]
            public string School { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;

            var userGrades = _dbContext.UserGrades.Where(g => g.UserId == user.Id);
            var userSubjects = _dbContext.UserSubjects.Where(g => g.UserId == user.Id);

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                School = user.School,
            };

            SelectedGrades = userGrades.Select(x => x.GradeId).ToArray();
            SelectedSubjects = userSubjects.Select(x => x.SubjectId).ToArray();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var grades = await _dbContext.Grades.ToListAsync();
            GradesList = new MultiSelectList(grades, nameof(Grade.Id), nameof(Grade.Title));
            var subjects = await _dbContext.Subjects.ToListAsync();
            SubjectsList = new MultiSelectList(subjects, nameof(Subject.Id), nameof(Subject.Title));

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // update FirstName if the FirstName form has been Changed
            if (!string.IsNullOrEmpty(Input.FirstName))
            {
                if (Input.FirstName != user.FirstName)
                {
                    user.FirstName = Input.FirstName;
                    var setFirstNameResult = await _userManager.UpdateAsync(user);

                    if (!setFirstNameResult.Succeeded)
                    {
                        StatusMessage = "Unexpected error when trying to set FirstName.";
                        return RedirectToPage();
                    }
                }
                
                StatusMessage = "Your First Name has been updated";
            }

            // update LastName if the LastName form has been Changed
            if (!string.IsNullOrEmpty(Input.LastName))
            {
                if (Input.LastName != user.LastName)
                {
                    user.LastName = Input.LastName;
                    var setLastNameResult = await _userManager.UpdateAsync(user);

                    if (!setLastNameResult.Succeeded)
                    {
                        StatusMessage = "Unexpected error when trying to set LastName.";
                        return RedirectToPage();
                    }
                }

                StatusMessage = "Your Last Name has been updated";
            }

            // update School if the School form has been Changed
            if (!string.IsNullOrEmpty(Input.School))
            {
                if (Input.School != user.School)
                {
                    user.School = Input.School;
                    var setSchoolResult = await _userManager.UpdateAsync(user);

                    if (!setSchoolResult.Succeeded)
                    {
                        StatusMessage = "Unexpected error when trying to set School.";
                        return RedirectToPage();
                    }
                }

                StatusMessage = "Your School has been updated";
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
                    var exists = _dbContext.UserGrades.Any(x => x.GradeId == gradeId && x.UserId == user.Id);

                    _dbContext.UserGrades.Add(new UserGrade() { GradeId = gradeId, UserId = user.Id });
                }
                _dbContext.SaveChanges();
                //await _userManager.UpdateAsync(user);
                _logger.LogInformation("User changed Grades successfully.");
                StatusMessage = "Grades Updated Successfully.";
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
                _logger.LogInformation("User changed Subjects successfully.");
                StatusMessage = "Subjects Updated Successfully.";
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Services;
using Microsoft.AspNetCore.Identity;
using CollegeManagement.Models.Student;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CollegeManagement.Controllers.Student
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentLogin(StudentLogin model, string? returnUrl = null)
        {
            return RedirectToAction("Index", "StudentDashboard");
            if (!ModelState.IsValid)
                return View(model);

            // Normal Identity login check
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // Fetch your student record (from StudentRepository)
                var student = _studentRepository.GetStudentByEmail(model.Email);

                // Build custom claims for your cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, student.Email),
                    new Claim(ClaimTypes.NameIdentifier, student.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Create authentication cookie with student claims
                await HttpContext.SignInAsync(
                    IdentityConstants.ApplicationScheme,
                    principal
                );

                // Return where user was going or default dashboard
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "StudentDashboard");
            }

            ModelState.AddModelError("", "Invalid Login Attempt");
            return View(model);
        }

    }
}


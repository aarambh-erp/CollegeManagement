using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Models.Student;
using CollegeManagement.Services.Student;
using CollegeManagement.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CollegeManagement.Models.ViewModels;
using CollegeManagement.Models.ViewModels.Profiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CollegeManagement.Controllers.Student
{
    //[Authorize]
    public class StudentDashboardController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentDashboardController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        private StudentInfo GetLoggedInStudent()
        {
            string studentIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(studentIdString, out int studentId))
                return null;
            return _studentRepository.GetStudentByid(studentId);
        }
        private IActionResult ViewWithStudent(string viewName)
        {
            var student = GetLoggedInStudent();
            if (student == null)
                return RedirectToAction("Login", "Account");

            return View(viewName, student);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            // int studentId = int.Parse(User.FindFirst("StudentId").Value);
            int studentId = 1;

            var courses = _studentRepository.GetCoursesByStudentId(studentId);
            var results = _studentRepository.GetResultsByStudentId(studentId);
            var student = _studentRepository.GetStudentByid(studentId);
            var upcomingAssgn = _studentRepository.GetUpcomingAssignmentsCount(studentId);

            var dashboardVM = new StudentDashboardViewModel
            {
                StudentName = student.Name,
                TotalCourses = courses.Count,

                UpcomingAssignments = upcomingAssgn,   // <-- Replace with real exam logic later

                AverageGrade = results.Any()
                                ? results.Average(r => r.MarksObtained)
                                : 0
            };

            return View(dashboardVM);
            ViewBag.ActiveTab = "Dashboard";
            return ViewWithStudent("Index");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            //return View(_studentRepository.GetStudentByid(1));
            var student = _studentRepository.GetStudentByid(1);
            var courses = _studentRepository.GetCoursesByStudentId(1);
            var model = new ProfileViewModel
            {

                Student = student,
                Courses = courses
            };
            return View(model);
            ViewBag.ActiveTab = "Profile";
            return ViewWithStudent("Profile");
        }

        [HttpGet]
        public IActionResult Courses()
        {
            ViewBag.ActiveTab = "Courses";
            return View(_studentRepository.GetCoursesByStudentId(1));
            return ViewWithStudent("Courses");
        }

        [HttpGet]
        public IActionResult Exams()
        {
            ViewBag.ActiveTab = "Exams";
            return ViewWithStudent("Exams");
        }
        [HttpGet]
        public IActionResult Assignments()
        {
            ViewBag.ActiveTab = "Assignment";
            return View(_studentRepository.GetAssignmentsByStudentId(1));
            return ViewWithStudent("Assignment");
        }
        [HttpGet]
        public IActionResult Timetable()
        {
            ViewBag.ActiveTab = "Timetable";
            return View(_studentRepository.GetTimetablesByStudentId(1));
            return ViewWithStudent("Timetable");
        }
        [HttpGet]
        public IActionResult Fees()
        {
            ViewBag.ActiveTab = "Fees";
            return View(_studentRepository.GetFeesByStudentId(1));  
            return ViewWithStudent("Fees");
        }
        [HttpGet]
        public IActionResult Results()
        {
            ViewBag.ActiveTab = "Results";
            return View(_studentRepository.GetResultsByStudentId(1));
            return ViewWithStudent("Results");
        }
        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            StudentInfo studentInfo = _studentRepository.GetStudentByid(id);
            EditProfileViewModel model = new EditProfileViewModel
            {
                Email = studentInfo.Email,
                PhoneNo = studentInfo.ContactNo,
                ExistingPhotoPath = studentInfo.PhotoPath
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("index");
            }
            return View();
        }
    }

}


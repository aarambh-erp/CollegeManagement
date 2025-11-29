using System;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Lecturers;
namespace CollegeManagement.Models.Courses
{
	public class Course
	{
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public int Credits { get; set; }
        public string? Status { get; set; }
        public string? Semester { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; }
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}


using System;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Courses;
namespace CollegeManagement.Models.ViewModels.Profiles
{
	public class ProfileViewModel
	{
		public StudentInfo Student { get; set; }
		public List<Course> Courses { get; set; }
	}
}


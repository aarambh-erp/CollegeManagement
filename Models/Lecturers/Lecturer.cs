using System;
using CollegeManagement.Models.Courses;

namespace CollegeManagement.Models.Lecturers
{
	public class Lecturer
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}


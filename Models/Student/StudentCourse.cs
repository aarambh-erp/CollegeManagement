using System;
using CollegeManagement.Models.Courses;

namespace CollegeManagement.Models.Student
{
	public class StudentCourse
	{
        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}


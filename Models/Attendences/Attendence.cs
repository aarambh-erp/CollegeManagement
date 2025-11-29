using System;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Student;

namespace CollegeManagement.Models.Attendences
{
	public class Attendence
	{
        public int Id { get; set; }

        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}


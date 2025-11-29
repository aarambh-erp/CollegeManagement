using System;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Student;

namespace CollegeManagement.Models.Results
{
	public class Result
	{
        public int Id { get; set; }
        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public double MarksObtained { get; set; }
        public double TotalMarks { get; set; }
        public double GPA { get; set; }

        public string? Semester { get; set; }
    }
}


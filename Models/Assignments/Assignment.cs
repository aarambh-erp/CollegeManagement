using System;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Lecturers;
using CollegeManagement.Models.Student;

namespace CollegeManagement.Models.Assignments
{
	public class Assignment
	{
        public int Id { get; set; }

        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public string? AssignmentFilePath { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}


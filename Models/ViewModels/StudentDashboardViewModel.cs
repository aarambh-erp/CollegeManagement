using System;
namespace CollegeManagement.Models.ViewModels
{
	public class StudentDashboardViewModel
	{
        public string? StudentName { get; set; }
        public int TotalCourses { get; set; }
        public int UpcomingAssignments { get; set; }
        public double AverageGrade { get; set; }
    }
}


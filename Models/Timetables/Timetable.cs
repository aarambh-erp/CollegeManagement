using System;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Rooms;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Lecturers;

namespace CollegeManagement.Models.Timetables
{
	public class Timetable
	{
        public int Id { get; set; }

        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}


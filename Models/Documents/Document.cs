using System;
using CollegeManagement.Models.Student;

namespace CollegeManagement.Models.Documents
{
	public class Document
	{
        public int Id { get; set; }

        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public string DocumentType { get; set; }   // ID Card, Bonafide, Result, etc.
        public string FilePath { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}


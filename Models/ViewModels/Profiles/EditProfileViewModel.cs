using System;
namespace CollegeManagement.Models.ViewModels.Profiles
{
	public class EditProfileViewModel
	{
		public string? Email { get; set; }
		public string? PhoneNo { get; set; }
		public string? ExistingPhotoPath { get; set; }
		public IFormFile? Photo { get; set; }
	}
}


using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Models.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter email or phone")]
        public string? Identifier { get; set; }

        public string? Otp { get; set; }

        // This controls whether OTP box is shown
        public bool IsOtpSent { get; set; }
    }
}

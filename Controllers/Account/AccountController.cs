using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Services.Email;
using CollegeManagement.Services.Sms;
using CollegeManagement.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(IEmailService emailService, ISmsService smsService, UserManager<IdentityUser> userManager)
        {
            _emailService = emailService;
            _smsService = smsService;
            _userManager = userManager;
        }

        // -----------------------------
        // FORGOT PASSWORD (GET)
        // -----------------------------
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ResetOTP")))
            {
                model.IsOtpSent = true;
                model.Identifier = HttpContext.Session.GetString("ResetIdentifier");
            }

            return View(model);
        }

        // -----------------------------
        // SEND OTP
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendOtp(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ForgotPassword");

            string otp = new Random().Next(100000, 999999).ToString();

            HttpContext.Session.SetString("ResetIdentifier", model.Identifier);
            HttpContext.Session.SetString("ResetOTP", otp);
            HttpContext.Session.SetString("OtpTime", DateTime.UtcNow.ToString());

            if (model.Identifier.Contains('@'))
                await _emailService.SendEmailAsync(model.Identifier, "Password Reset OTP", $"Your OTP is <b>{otp}</b>. Valid for 5 minutes.");
            else
                await _smsService.SendSmsAsync(model.Identifier, $"Your OTP is {otp}. Valid for 5 minutes.");

            TempData["Message"] = $"OTP sent to {model.Identifier}!";
            Console.WriteLine("$ The opt is {otp}");
            Console.WriteLine("$The phone number is {model.Identifier}");
            return RedirectToAction("ForgotPassword");
        }

        // -----------------------------
        // VERIFY OTP
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyOtp(ForgotPasswordViewModel model)
        {
            var savedOtp = HttpContext.Session.GetString("ResetOTP");
            var savedIdentifier = HttpContext.Session.GetString("ResetIdentifier");
            var otpTimeStr = HttpContext.Session.GetString("OtpTime");

            if (savedOtp == null || savedIdentifier == null)
            {
                ModelState.AddModelError("", "Please request OTP first.");
                model.IsOtpSent = true;
                return View("ForgotPassword", model);
            }

            if (!DateTime.TryParse(otpTimeStr, out DateTime otpTime))
            {
                ModelState.AddModelError("", "Invalid OTP session. Please request OTP again.");
                model.IsOtpSent = false;
                return View("ForgotPassword", model);
            }

            // Check expiry
            if (DateTime.UtcNow - otpTime > TimeSpan.FromMinutes(5))
            {
                ModelState.AddModelError("", "OTP expired. Please request a new one.");
                HttpContext.Session.Clear();
                model.IsOtpSent = false;
                return View("ForgotPassword", model);
            }

            // Check OTP match
            if (model.Otp?.Trim() != savedOtp?.Trim())
            {
                ModelState.AddModelError("", "Invalid OTP. Try again.");
                model.IsOtpSent = true;
                return View("ForgotPassword", model);
            }

            HttpContext.Session.Remove("ResetOTP");
            HttpContext.Session.Remove("OtpTime");

            // Find user
            IdentityUser user = null;
            if (model.Identifier.Contains('@'))
                user = await _userManager.FindByEmailAsync(model.Identifier);
            else
                user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.Identifier);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                model.IsOtpSent = true;
                return View("ForgotPassword", model);
            }

            // Generate reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            HttpContext.Session.SetString("ResetToken", token);
            HttpContext.Session.SetString("ResetUserId", user.Id);
            return RedirectToAction("ResetPassword");
        }

        // -----------------------------
        // RESET PASSWORD (GET)
        // -----------------------------
        [HttpGet]
        public IActionResult ResetPassword()
        {
            var token = HttpContext.Session.GetString("ResetToken");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("No reset token found. Redirecting to ForgotPassword");
                return RedirectToAction("ForgotPassword");
            }

            return View(new ResetPasswordViewModel());
        }

        // -----------------------------
        // RESET PASSWORD (POST)
        // -----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("ResetToken");
            var userId = HttpContext.Session.GetString("ResetUserId");

            if (token == null || userId == null)
                return RedirectToAction("ForgotPassword");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction("ForgotPassword");

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                Console.WriteLine("Password reset successful. Clearing session.");
                HttpContext.Session.Clear();
                return RedirectToAction("StudentLogin", "Student");
            }

            foreach (var err in result.Errors)
                ModelState.AddModelError("", err.Description);

            return View(model);
        }
    }
}

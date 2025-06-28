using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.BusinessObjects;
using POS_API.Interfaces;
using POS_API.Model;
using POS_API.Services;

namespace POS_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SQLService _sqlService;
        private readonly POSEntities db;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(SQLService sqlService, DbContextOptions<POSEntities> options, IAuthService authService, IEmailService emailService)
        {
            _sqlService = sqlService;
            db = new POSEntities(options);
            _authService = authService;
            _emailService = emailService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var token = await _authService.AuthenticateAsync(req);
            return Ok(new { token, status= 200, message="Successfully Logged In" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            if (string.IsNullOrWhiteSpace(forgotPassword.email))
            {
                return BadRequest(new { status = 400, message = "Email is required" });
            }
            var user = await db.tblUsers.FirstOrDefaultAsync(u => u.varEmail == forgotPassword.email && u.varAuthProvider == "Local");
            if (user == null)
            {
                return NotFound(new { status = 404, message = "User not found" });
            }
            var code = new Random().Next(1000, 9999).ToString();
            var subject = "Your Password Reset Verification Code";
            var body = $"Your verification code is: {code}";
            bool success = await _emailService.sendEmail(user.varEmail, subject, body);
            if (success)
            {
                var verificationCode = new tblVerificationCode
                {
                    UserId = user.intUserId,
                    Code = code,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    IsUsed = false,
                    CreatedAt = DateTime.UtcNow,
                    User = user
                };

                db.tblVerificationCode.Add(verificationCode);
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Verification code sent to your email", verificationCode });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = 500, message = "Failed to send email" });
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest req)
        {
            var token = await _authService.GoogleAuthenticateAsync(req.IdToken);
            return Ok(new { token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPasswordRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(resetPasswordRequest.newPassword))
                {
                    return BadRequest(new { status = 400, message = "Password is Required" });
                }
                var user = await db.tblUsers.FirstOrDefaultAsync(u => u.intUserId == resetPasswordRequest.userId);
                if(user == null)
                {
                    return NotFound(new { status = 404, message = "User not found" });
                }
                user.varPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequest.newPassword);
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(new { status = 200, message = "Successfully Updated Password" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerificationCodeRequest verificationCodeRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(verificationCodeRequest.code))
                {
                    return BadRequest(new { status = 400, message = "Verification code is required" });
                }

                if (string.IsNullOrEmpty(verificationCodeRequest.email))
                {
                    return BadRequest(new { status = 400, message = "Email is required" });
                }

                // Find the user by email
                var user = await db.tblUsers.FirstOrDefaultAsync(u => u.varEmail == verificationCodeRequest.email);
                if (user == null)
                {
                    return NotFound(new { status = 404, message = "User not found" });
                }

                // Find the verification code for this user
                var verificationCode = await db.tblVerificationCode
                    .Where(vc => vc.UserId == user.intUserId &&
                                vc.Code == verificationCodeRequest.code &&
                                !vc.IsUsed &&
                                vc.ExpiresAt > DateTime.UtcNow)
                    .OrderByDescending(vc => vc.CreatedAt)
                    .FirstOrDefaultAsync();

                if (verificationCode == null)
                {
                    return BadRequest(new { status = 400, message = "Invalid or expired verification code" });
                }

                // Mark the verification code as used
                verificationCode.IsUsed = true;
                db.Entry(verificationCode).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Verification code verified successfully",
                    userId = user.intUserId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = 500,
                    message = "An error occurred while verifying the code",
                    error = ex.Message
                });
            }
        }

    }



    
}

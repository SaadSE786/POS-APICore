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
        public AuthController(SQLService sqlService, DbContextOptions<POSEntities> options, IAuthService authService)
        {
            _sqlService = sqlService;
            db = new POSEntities(options);
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var token = await _authService.AuthenticateAsync(req);
            return Ok(new { token, status= 200, message="Successfully Logged In" });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest req)
        {
            var token = await _authService.GoogleAuthenticateAsync(req.IdToken);
            return Ok(new { token });
        }
    }

    
}

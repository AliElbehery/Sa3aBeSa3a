using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Application.Auth;

namespace Sa3aBeSa3a.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthServices auth, IAuthRepository authRepository) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        public async Task<ActionResult<AuthResultDtos>> Register(RegisterDto dto)
        => StatusCode(StatusCodes.Status201Created, await auth.RegisterAsync(dto));

        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        public async Task<ActionResult<AuthResultDtos>> Login(LoginDto dto)
        => await auth.LoginAsync(dto);

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<MemberDto>> Me()
       => await authRepository.GetProfileAsync(User.GetUserId());
    }
}

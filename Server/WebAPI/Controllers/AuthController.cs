using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using Entities;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepo;

        public AuthController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequest request)
        {
            User user = await userRepo.GetSingleByUsernameAsync(request.UserName);

            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            UserDTO dto = new()
            {
                UserId = user.UserId,
                Username = user.Username ?? string.Empty

            };

            return Ok(dto);
        }
    }
}

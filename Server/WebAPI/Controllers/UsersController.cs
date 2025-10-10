using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }


        // Create new user
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] CreateUserDTO request)
        {
            // await VerifyUserNameIsAvailableAsync(request.UserName);
            User user = new(request.UserName, request.Password);
            User created = await userRepo.AddAsync(user);
            UserDTO dto = new()
            {
                UserId = created.UserId,
                Username = created.Username ?? string.Empty
            };
            return Created($"/users{dto.UserId}", dto);
        }

        // Get a single user by userId
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] int userId)
        {
            User user = await userRepo.GetSingleAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            UserDTO dto = new()
            {
                UserId = user.UserId,
                Username = user.Username ?? string.Empty
            };

            return Ok(dto);
        }

        // Get Many Users
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetManyUsers([FromQuery] string? usernameContains = null)
        {
            IQueryable<User> users = userRepo.GetMany();

            if (!string.IsNullOrEmpty(usernameContains))
            {
                users = users
                .Where(u => u.Username != null && u.Username.Contains(usernameContains, StringComparison.OrdinalIgnoreCase));
            }

            List<UserDTO> dtos = users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                Username = u.Username ?? string.Empty
            }).ToList();
            return Ok(dtos);
        }

        // Delete a user
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] int userId)
        {
            User existingUser = await userRepo.GetSingleAsync(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await userRepo.DeleteAsync(userId);
            UserDTO dto = new()
            {
                UserId = existingUser.UserId,
                Username = existingUser.Username ?? string.Empty
            };
            return Ok(dto);
        }
        // Update an existing user
        [HttpPut("{userId:int}")]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] int userId, [FromBody] CreateUserDTO request)
        {
            User existingUser = await userRepo.GetSingleAsync(userId);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Username = request.UserName;
            existingUser.Password = request.Password;

            await userRepo.UpdateAsync(existingUser);
            UserDTO dto = new()
            {
                UserId = existingUser.UserId,
                Username = existingUser.Username ?? string.Empty
            };  
            return Ok(dto);
        }


    }
}

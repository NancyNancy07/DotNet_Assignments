using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(CreateUserDTO request); 
    // public Task UpdateUserAsync(int id, UpdateUserDto request);
}

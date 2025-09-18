using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{

    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter Username: ");
        string? username = Console.ReadLine();

        Console.WriteLine("Enter Password: ");
        string? password = Console.ReadLine();

        var user = new User();
        user.Username = username;
        user.Password = password;

        await userRepository.AddAsync(user);
        Console.WriteLine($"User: {username}, with ID {user.UserId} is created");
    }

}

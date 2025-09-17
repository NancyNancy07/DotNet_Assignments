using System;
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
        Console.WriteLine("user created");
        await Task.CompletedTask;
    }

}

using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{private readonly IUserRepository userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    
    public async Task ShowAsync()
    {
        Console.WriteLine("single user is here");
        await Task.CompletedTask;
    }

}

using RepositoryContracts;
namespace CLI.UI.ManageUsers;

public class ListUsersView
{
private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    
    public async Task ShowAsync()
    {
        Console.WriteLine("All users are here");
        await Task.CompletedTask;
    }

}

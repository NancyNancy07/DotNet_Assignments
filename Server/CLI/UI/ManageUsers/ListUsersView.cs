using RepositoryContracts;
namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }


    public void ShowList()
    {
        Console.WriteLine("All users are here");
    }

}

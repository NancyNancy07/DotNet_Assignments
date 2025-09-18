using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly IUserRepository userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }


    public async Task ShowAsync()
    {
        Console.WriteLine("Enter UserID to view details: ");

        string? id = Console.ReadLine();
        int userId = int.Parse(id!);

        var user = await userRepository.GetSingleAsync(userId);

        Console.WriteLine($"User found: Username: {user.Username}");

    }

}

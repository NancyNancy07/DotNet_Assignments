using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUser;
    private readonly ListUsersView listUsers;
    private readonly SingleUserView singleUser;

     public ManageUsersView(IUserRepository userRepository)
    {
        this.createUser = new CreateUserView(userRepository);
        this.listUsers = new ListUsersView(userRepository);
        this.singleUser = new SingleUserView(userRepository);
    }
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("---- Manage Users ----");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("3. View Single User");
            Console.WriteLine("0. Back to Main Menu");


            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateUserAsync();
                    break;
                case "2":
                    UserList();
                    break;
                case "3":
                    await SingleUserAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
    public async Task CreateUserAsync()
    {
        await createUser.ShowAsync();
    }

    public void UserList()
    {
        listUsers.ShowList();
    }

    public async Task SingleUserAsync()
    {
        await singleUser.ShowAsync();
    }
}

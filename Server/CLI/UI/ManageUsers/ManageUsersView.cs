namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUser;
    private readonly ListUsersView listUsers;
    private readonly SingleUserView singleUser;

    public ManageUsersView(CreateUserView createUser, ListUsersView listUsers, SingleUserView singleUser)
    {
        this.createUser = createUser;
        this.listUsers = listUsers;
        this.singleUser = singleUser;
    }

    async public Task CreateUserAsync()
    {
        await createUser.ShowAsync();
    }

     public void UserList()
    {
         listUsers.ShowList();
    }

    async public Task SingleUserAsync()
    {
        await singleUser.ShowAsync();
    }
}

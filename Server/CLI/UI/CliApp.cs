using CLI.UI.ManageUsers;
using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly ManageUsersView manageUsers;
    private readonly ManagePostsView managePosts;
    private readonly ManageCommentsView manageComments;

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        var createUser = new CreateUserView(userRepository);
        var userList = new ListUsersView(userRepository);
        var singleUser = new SingleUserView(userRepository);

        this.manageUsers = new ManageUsersView(userRepository);

        var createPost = new CreatePostView(postRepository);
        var postList = new ListPostsView(postRepository);
        var singlePost = new SinglePostView(postRepository, commentRepository);

        this.managePosts = new ManagePostsView(postRepository,commentRepository);

        var createComment = new CreateCommentView(commentRepository);
        var commentList = new ListCommentsView(commentRepository);
        var singleComment = new SingleCommentView(commentRepository);

        this.manageComments = new ManageCommentsView(commentRepository);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("----Main Menu----");
            Console.WriteLine("1. Managers Users");
            Console.WriteLine("2. Manage Posts");
            Console.WriteLine("3. Manage Comments");
            Console.WriteLine("0. Exit");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await manageUsers.StartAsync(); // go into Users menu
                    break;

                case "2":
                    await managePosts.StartAsync(); // go into Posts menu
                    break;

                case "3":
                    await manageComments.StartAsync(); // go into Comments menu
                    break;
                case "0":
                    Console.WriteLine("exiting...");
                    return;
            }

        }
    }
}

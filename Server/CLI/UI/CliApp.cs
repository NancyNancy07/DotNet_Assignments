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

        this.manageUsers = new ManageUsersView(createUser, userList, singleUser);

        var createPost = new CreatePostView(postRepository);
        var postList = new ListPostsView(postRepository);
        var singlePost = new SinglePostView(postRepository,commentRepository);

        this.managePosts = new ManagePostsView(createPost, postList, singlePost);

        var createComment = new CreateCommentView(commentRepository);
        var commentList = new ListCommentsView(commentRepository);
        var singleComment = new SingleCommentView(commentRepository);

        this.manageComments = new ManageCommentsView(createComment, commentList, singleComment);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("Select from given Options(0-5):");
            Console.WriteLine("1. Create new User");
            Console.WriteLine("2. Create new Post");
            Console.WriteLine("3. Add Comment");
            Console.WriteLine("4. View All Posts");
            Console.WriteLine("5. View Single Post");
            Console.WriteLine("0. Exit");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await manageUsers.CreateUserAsync();
                    break;

                case "2":
                    await managePosts.CreatePostAsync();
                    break;
                case "3":
                    await manageComments.CreateCommentAsync();
                    break;

                case "4":
                     managePosts.PostList();
                    break;

                case "5":
                    await managePosts.SinglePostAsync();
                    break;

                case "0":
                    Console.WriteLine("exiting...");
                    return;
            }

        }
    }
}

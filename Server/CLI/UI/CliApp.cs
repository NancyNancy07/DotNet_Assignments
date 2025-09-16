using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
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
                    await CreateUserAsync();
                    break;

                case "2":
                    await CreatePostAsync();
                    break;
                case "3":
                    await AddCommentAsync();
                    break;

                case "4":
                    await ListPostsAsync();
                    break;

                case "5":
                    await SinglePostAsync();
                    break;

                case "0":
                    Console.WriteLine("exiting...");
                    return;
            }

        }
    }


    public async Task CreateUserAsync()
    {
        Console.WriteLine("user created");
        await Task.CompletedTask;
    }

    public async Task CreatePostAsync()
    {
        Console.WriteLine("post created");
        await Task.CompletedTask;

    }

    public async Task AddCommentAsync()
    {
        Console.WriteLine("comment added");
        await Task.CompletedTask;
    }
    public async Task ListPostsAsync()
    {
        Console.WriteLine("all posts are here");
        await Task.CompletedTask;
    }
    public async Task SinglePostAsync()
    {
        Console.WriteLine("single post is here");
        await Task.CompletedTask;
    }


}

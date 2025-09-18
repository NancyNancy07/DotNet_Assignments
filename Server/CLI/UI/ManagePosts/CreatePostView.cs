using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{

    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Write Post details: ");

        Console.WriteLine("Enter UserID: ");

        string? id = Console.ReadLine();
        int userId = int.Parse(id!);

        Console.WriteLine("Title: ");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter Body Text: ");
        string? body = Console.ReadLine();

        var newPost = new Post();
        newPost.Title = title;
        newPost.Body = body;
        newPost.UserId = userId;

        await postRepository.AddAsync(newPost);

        Console.WriteLine($"Post: Title: {title}, Body: {body} is created by {userId} ");

    }

}

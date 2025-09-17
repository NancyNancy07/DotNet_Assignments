using RepositoryContracts;

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
        Console.WriteLine("post created");
        await Task.CompletedTask;

    }

}

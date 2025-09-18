using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;


    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter PostID to view details: ");

        string? id = Console.ReadLine();
        int postId = int.Parse(id!);

        var post = await postRepository.GetSingleAsync(postId);
        var comment = await commentRepository.GetSingleAsync(postId);

        Console.WriteLine($"Post: title: {post.Title} body: {post.Body} comment: {comment.Body}");
    }
}

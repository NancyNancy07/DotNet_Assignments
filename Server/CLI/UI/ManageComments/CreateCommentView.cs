using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{

    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Add a comment to Post: ");

        Console.WriteLine("Enter Post ID: ");
        string? stringPostId = Console.ReadLine();
        int postId = int.Parse(stringPostId!);

        Console.WriteLine("Enter UserID: ");
        string? stringUserId = Console.ReadLine();
        int userId = int.Parse(stringUserId!);

        Console.WriteLine("Enter Comment: ");
        string? comment = Console.ReadLine();

        var newComment = new Comment();
        newComment.Postid = postId;
        newComment.Userid = userId;
        newComment.Body = comment;
        await commentRepository.AddAsync(newComment);

        Console.WriteLine($"Comment: {comment} is added on post {postId} by user {userId}");
    }

}

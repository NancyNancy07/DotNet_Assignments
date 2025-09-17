using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class SingleCommentView
{
   private readonly ICommentRepository commentRepository;

    public SingleCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("single comment is here");
        await Task.CompletedTask;
    }
}

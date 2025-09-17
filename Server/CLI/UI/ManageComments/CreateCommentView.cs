using System;
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
        Console.WriteLine("comment added");
        await Task.CompletedTask;
    }

}

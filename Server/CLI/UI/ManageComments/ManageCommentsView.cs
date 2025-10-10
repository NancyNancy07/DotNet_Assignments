using System;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly CreateCommentView createComment;
    private readonly ListCommentsView listComments;
    private readonly SingleCommentView singleComment;

       public ManageCommentsView(ICommentRepository commentRepository)
    {
        this.createComment = new CreateCommentView(commentRepository);
        this.listComments = new ListCommentsView(commentRepository);
        this.singleComment = new SingleCommentView(commentRepository);
    }
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("---- Manage Comments ----");
            Console.WriteLine("1. Create Comment");
            Console.WriteLine("2. List Comments");
            Console.WriteLine("3. View Single Comment");
            Console.WriteLine("0. Back to Main Menu");


            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateCommentAsync();
                    break;
                case "2":
                    CommentList();
                    break;
                case "3":
                    await SingleCommentAsync();
                    break;
                case "0":
                    return;
            }
        }
    }

    public async Task CreateCommentAsync()
    {
        await createComment.ShowAsync();
    }

    public void CommentList()
    {
        listComments.ShowList();
    }

    public async Task SingleCommentAsync()
    {
        await singleComment.ShowAsync();
    }
}

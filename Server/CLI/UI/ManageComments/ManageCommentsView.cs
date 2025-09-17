using System;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
  private readonly CreateCommentView createComment;
    private readonly ListCommentsView listComments;
    private readonly SingleCommentView singleComment;

    public ManageCommentsView(CreateCommentView createComment, ListCommentsView listComments, SingleCommentView singleComment)
    {
        this.createComment= createComment;
        this.listComments = listComments;
        this.singleComment = singleComment;
    }

    async public Task CreateCommentAsync()
    {
        await createComment.ShowAsync();
    }

    async public Task CommentListAsync()
    {
        await listComments.ShowAsync();
    }

    async public Task SingleCommentAsync()
    {
        await singleComment.ShowAsync();
    }
}

using System;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
  private readonly CreatePostView createPost;
    private readonly ListPostsView listPosts;
    private readonly SinglePostView singlePost;

    public ManagePostsView(CreatePostView createPost, ListPostsView listPosts, SinglePostView singlePost)
    {
        this.createPost= createPost;
        this.listPosts = listPosts;
        this.singlePost = singlePost;
    }

    async public Task CreatePostAsync()
    {
        await createPost.ShowAsync();
    }

     public void PostList()
    {
     listPosts.ShowList();
    }

    async public Task SinglePostAsync()
    {
        await singlePost.ShowAsync();
    }
}

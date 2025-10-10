using System;
using RepositoryContracts;
namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly CreatePostView createPost;
    private readonly ListPostsView listPosts;
    private readonly SinglePostView singlePost;

  public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.createPost = new CreatePostView(postRepository);
        this.listPosts = new ListPostsView(postRepository);
        this.singlePost = new SinglePostView(postRepository, commentRepository);
    }
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("---- Manage Posts ----");
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. List Posts");
            Console.WriteLine("3. View Single Post");
            Console.WriteLine("0. Back to Main Menu");


            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreatePostAsync();
                    break;
                case "2":
                    PostList();
                    break;
                case "3":
                    await SinglePostAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
  
    public async Task CreatePostAsync()
    {
        await createPost.ShowAsync();
    }

    public void PostList()
    {
        listPosts.ShowList();
    }

    public async Task SinglePostAsync()
    {
        await singlePost.ShowAsync();
    }
}

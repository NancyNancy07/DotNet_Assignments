using RepositoryContracts;
using Entities;
namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void ShowList()
    {
        IQueryable<Post> result = postRepository.GetMany();

        foreach (var post in result)
        {
            Console.WriteLine($"Post: \nPostId: {post.PostId}, Post Title: {post.Title}");
        }
    }
}

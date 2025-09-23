using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting the CLI App..."); 
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();


CliApp cliApp = new (userRepository, commentRepository, postRepository);
await cliApp.StartAsync();
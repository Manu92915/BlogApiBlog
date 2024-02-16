namespace BlogApi.Interfaces
{
    public interface ISeedRepository
    {
        Task<List<User>> GetAllUsers();
        Task<List<Post>> GetAllPosts();

        Task SaveUsers(List<User> users);
        Task SavePosts(List<Post> posts);

        Task<User?> ExistsUser(int id);
    }
}

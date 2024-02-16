namespace BlogApi.Repositories
{
    public class SeedRepository : ISeedRepository
    {
        private readonly DataContext _datacontext;
        public SeedRepository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _datacontext.Posts.ToListAsync();
            return posts;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _datacontext.Users.ToListAsync();
            return users;
        }

        public async Task SavePosts(List<Post> posts)
        {
            _datacontext.AddRange(posts);
            await _datacontext.SaveChangesAsync();
        }

        public async Task SaveUsers(List<User> users)
        {
            _datacontext.AddRange(users);
            await _datacontext.SaveChangesAsync();
        }

        public async Task<User?> ExistsUser(int id)
        {
            var user = await _datacontext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return null;
            }
            return user;
        }
    }
}

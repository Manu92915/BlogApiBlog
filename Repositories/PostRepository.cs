

namespace BlogApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public PostRepository(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<GetPostsDto> CreatePost(CreatePostsDto newPost)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Id == newPost.UserId); //check user
            if (user is null)
            {
                return null;
            }

            var post = _mapper.Map<Post>(newPost);
            post.User = user;
            _dataContext.Posts.Add(post);

            await _dataContext.SaveChangesAsync();

            var postDto = _mapper.Map<GetPostsDto>(post);
            postDto.UserFullName = user.FullName;

            return postDto;


        }

        public async Task<GetPostsDto> DeletePost(int id)
        {
            var post = _dataContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post is null)
            {
                return null;
            }
            _dataContext.Posts.Remove(post);
            await _dataContext.SaveChangesAsync();
            var postDto = _mapper.Map<GetPostsDto>(post);
            
            return postDto;



        }

        public async Task<List<GetPostsDto>> GetAllPosts()
        {
            var posts = await _dataContext.Posts.Include(f => f.User).Select(p => _mapper.Map<GetPostsDto>(p)).ToListAsync();

            return posts;
        }

        public async Task<List<GetPostsDto>> GetPaging(int skip, int take)
        {
            var posts = await _dataContext.Posts.Include(p => p.User).Skip(skip).Take(take).Select(p => _mapper.Map<GetPostsDto>(p)).ToListAsync();
            
            return posts;
        }

        public async Task<List<GetPostsDto>> GetUserPosts(int id)
        {
            var userPost = await _dataContext.Posts.Where(p => p.User!.Id == id).Include(u => u.User).ToListAsync();
            if (userPost.Count == 0)
            {
                return null;
            }

            var userPostDto = _mapper.Map<List<GetPostsDto>>(userPost);

            return userPostDto;

        }

        public async Task<GetPostsDto> UpdatePost(UpdatePostsDto updatePost)
        {
            var post = _dataContext.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == updatePost.Id);
            if (post is null)
            {
                return null;
            }

            post.Title = updatePost.Title;
            post.Body = updatePost.Body;

            await _dataContext.SaveChangesAsync();

            var postDto = _mapper.Map<GetPostsDto>(post);

            return postDto;



        }
    }
}

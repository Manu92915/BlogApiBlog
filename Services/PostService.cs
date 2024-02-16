
namespace BlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<GetPostsDto> CreatePost(CreatePostsDto newPost)
        {
            var post = await _postRepository.CreatePost(newPost); //check user
            
            return post;

            
        }

        public async Task<GetPostsDto> DeletePost(int id)
        {
            var postDto = await _postRepository.DeletePost(id);

            return postDto;



        }

        public async Task<List<GetPostsDto>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPosts();

            return posts;
        }

        public async Task<List<GetPostsDto>> GetPaging(int skip, int take = 10)
        {
            var posts = await _postRepository.GetPaging(skip, take);

            return posts;
        }

        public async Task<List<GetPostsDto>> GetUserPosts(int id)
        {
            var userPosts = await _postRepository.GetUserPosts(id);

            return userPosts;

        }

        public async Task<GetPostsDto> UpdatePost(UpdatePostsDto updatePost)
        {
            var post = await _postRepository.UpdatePost(updatePost);
            
            return post;    
          


        }

        
    }
}

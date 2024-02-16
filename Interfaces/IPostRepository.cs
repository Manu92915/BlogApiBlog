
namespace BlogApi.Interfaces
{
    public interface IPostRepository
    {
        Task<List<GetPostsDto>> GetAllPosts();
        Task<List<GetPostsDto>> GetUserPosts(int id);
        Task<GetPostsDto> CreatePost(CreatePostsDto newPost);
        Task<GetPostsDto> UpdatePost(UpdatePostsDto updatePost);
        Task<GetPostsDto> DeletePost(int id);

        Task<List<GetPostsDto>> GetPaging(int skip, int take);
    }
}

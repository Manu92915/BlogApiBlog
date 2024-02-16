

namespace BlogApi
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<Post, GetPostsDto>();
            CreateMap<CreatePostsDto, Post>();
        }
    }
}

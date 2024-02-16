

namespace BlogApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<UserGetDto>> GetAllUsers()
        {
            return await _dataContext.Users.AsNoTracking().Select(u => _mapper.Map<UserGetDto>(u)).ToListAsync();
        }

        public async Task<List<UserGetDto>> SearchUser(string value)
        {
            var targetUsers = await _dataContext.Users
           .Where(u => u.FirstName.Contains(value) || u.LastName.Contains(value))
           .ToListAsync();

            var targetUsersDto = _mapper.Map<List<UserGetDto>>(targetUsers);

            return targetUsersDto;
        }
    }
}

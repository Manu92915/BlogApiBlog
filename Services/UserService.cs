namespace BlogApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<List<UserGetDto>> GetAllUsers()
        {
           var users =  await _userRepository.GetAllUsers();
           return users;
        }

        public async Task<List<UserGetDto>> SearchUser(string value)
        {
            var users = await _userRepository.SearchUser(value);
            return users;
        }
    }
}

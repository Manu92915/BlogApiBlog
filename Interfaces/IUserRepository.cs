
namespace BlogApi.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserGetDto>> GetAllUsers();
        Task<List<UserGetDto>> SearchUser(string value);
    }
}

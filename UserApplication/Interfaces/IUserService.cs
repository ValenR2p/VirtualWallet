using UserApplication.Response;

namespace UserApplication.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(int id);
        Task<UserResponse> DeleteUser(int userId);
    }
}

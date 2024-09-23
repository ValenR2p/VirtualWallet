using Domain.Models;

namespace UserApplication.Interfaces
{
    public interface IUserQuery
    {
        Task<User> GetUserById(int id);
    }
}

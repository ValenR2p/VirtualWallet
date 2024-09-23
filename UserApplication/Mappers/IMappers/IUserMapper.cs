using Domain.Models;
using UserApplication.Response;

namespace UserApplication.Mappers.IMappers
{
    public interface IUserMapper
    {
        Task<UserResponse> GetUserResponse(User user);
    }

}

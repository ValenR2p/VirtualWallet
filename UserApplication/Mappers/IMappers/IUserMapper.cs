using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Response;

namespace UserApplication.Mappers.IMappers
{
    public interface IUserMapper
    {
        Task<UserResponse> GetUserResponse(User user);
    }

}

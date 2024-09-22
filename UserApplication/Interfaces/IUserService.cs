using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Response;

namespace UserApplication.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(int id);
        Task<UserResponse> DeleteUser(int userId);
    }
}

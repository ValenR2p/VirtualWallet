using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Mappers.IMappers;
using UserApplication.Response;

namespace UserApplication.Mappers
{
    public class UserMapper : IUserMapper
    {
        public Task<UserResponse> GetUserResponse(User user)
        {
            var response = new UserResponse
            {
                Adress = user.Adress,
                BirthDate = user.BirthDate,
                City = user.City,
                Country = user.Country,
                Deleted = user.Deleted,
                DNI = user.DNI,
                Email = user.Email,
                //Id = user.Id,
                LastLogin = user.LastLogin,
                LastName = user.LastName,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
            };
            return Task.FromResult(response);
        }
    }
}

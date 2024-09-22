using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Exceptions;
using UserApplication.Interfaces;
using UserApplication.Mappers.IMappers;
using UserApplication.Response;

namespace UserApplication.UseCases
{
    public class UserService:IUserService
    {
        private readonly IUserCommand _userCommand;
        private readonly IUserQuery _userQuery;
        private readonly IUserMapper _userMapper;
        
        public UserService(IUserCommand userCommand, IUserQuery userQuery, IUserMapper userMapper)
        {
            _userCommand = userCommand;
            _userQuery = userQuery;
            _userMapper = userMapper;
        }

        public async Task<UserResponse> DeleteUser(int userId)
        {
            await CheckUserId(userId);

            var softDelete = await _userCommand.DeleteUser(userId);
            return await _userMapper.GetUserResponse(softDelete);

        }

        public Task<UserResponse> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
        private async Task<bool> CheckUserId(int id)
        {
            if (await _userQuery.GetUserById(id)==null)
            {
                throw new ExceptionNotFound("No Existe User con ese id");
            }
            return false;
        }
    }
}

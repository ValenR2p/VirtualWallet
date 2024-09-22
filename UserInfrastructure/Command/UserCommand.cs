using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Interfaces;
using UserInfrastructure.Persistence;

namespace UserInfrastructure.Command
{
    public class UserCommand:IUserCommand
    {
        private readonly UserContext _context;
        public UserCommand(UserContext context)
        {
            _context = context;
        }

        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> DeleteUser(int UserId)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            userToDelete.Deleted = true;
            await _context.SaveChangesAsync();
            return userToDelete;
        }
    }
}

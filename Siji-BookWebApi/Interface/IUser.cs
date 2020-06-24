using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siji_BookWebApi.Entities;

namespace Siji_BookWebApi.Interface
{
    public interface IUser
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
        Task<bool> Update(User user, string password = null);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}

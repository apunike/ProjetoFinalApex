using Data.Models;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void CreatUser(User user);
        List<User> GetUsers();
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}

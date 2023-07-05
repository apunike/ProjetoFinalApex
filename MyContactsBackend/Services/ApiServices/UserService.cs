using Data.Interfaces;
using Data.Models;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.ApiServices
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public void CreatUser(User user)
        {
            UserRepository.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            UserRepository.DeleteUser(user);
        }

        public List<User> GetUsers()
        {
            return UserRepository.GetUsers();
        }

        public void UpdateUser(User user)
        {
            UserRepository.UpdateUser(user);
        }
    }
}

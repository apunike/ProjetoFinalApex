using Data.Interfaces;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser()
        {
            
        }

        public void DeleteUser()
        {
            
        }

        public List<string> GetUsers()
        {
            return new List<string>(){ "joao", "Maria", " Paulo"};
        }

        public void UpdateUser()
        {
           
        }
    }
}

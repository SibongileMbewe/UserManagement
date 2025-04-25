using System;
using System.Collections.Generic;

namespace UserManagement.Core.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
    }
}

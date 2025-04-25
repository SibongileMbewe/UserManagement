using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UserManagement.Core;
using UserManagement.Core.Repositories;

namespace UserManagement.Infrastructure
{
    public class XmlUserRepository : IUserRepository
    {
        private const string _filePath = "users.xml";
        public XmlUserRepository() { }


        public void AddUser(User user)
        {
            var users = GetAllUsers();

            // Check for an exact duplicate of all three fields
            bool isExactDuplicate = users.Any(u =>
                u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase) &&
                u.Surname.Equals(user.Surname, StringComparison.OrdinalIgnoreCase) &&
                u.Cellphone == user.Cellphone
            );

            if (isExactDuplicate)
                throw new InvalidOperationException("A user with the same name, surname, and cellphone already exists.");

            // Check if cellphone is already in use by a different person
            bool isCellphoneDuplicate = users.Any(u =>
                u.Cellphone == user.Cellphone
            );

            if (isCellphoneDuplicate)
                throw new InvalidOperationException("This cellphone number is already in use.");

            user.Id = Guid.NewGuid(); 
            users.Add(user);
            SaveUsers(users);
        }


        public void DeleteUser(Guid id)
        {
            var users = GetAllUsers();
            users.RemoveAll(u => u.Id == id);
            SaveUsers(users);
        }

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            try
            {
                var serializer = new XmlSerializer(typeof(List<User>));
                using (var reader = new StreamReader(_filePath))
                {
                    var deserializedUsers = serializer.Deserialize(reader) as List<User>;
                    return deserializedUsers ?? new List<User>();
                }
            }
            catch
            {
                return new List<User>();
            }
        }

        public User GetUserById(Guid id)
        {
            return GetAllUsers().FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            var users = GetAllUsers();
            var index = users.FindIndex(u => u.Id == user.Id);
                        
            if (index != -1)
            {
                users[index] = user;
                SaveUsers(users);
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }

        private void SaveUsers(List<User> users)
        {
            var serializer = new XmlSerializer(typeof(List<User>));
            using (var writer = new StreamWriter(_filePath))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}

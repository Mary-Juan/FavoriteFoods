using HW7.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW7.Storage;
using HW7.Utility;
using HW7.Services.Interfaces;
using HW7.Exeptions;

namespace HW7.Services
{
    internal class UserService : IUserService
    {

        private string _filePath;
        private List<User> _users;
        private Serialization<User> _serialization;

        public UserService(string filePath)
        {
            _serialization = new Serialization<User>();
            _filePath = filePath;
            _users = _serialization.DeserializeFromJsonFile(_filePath);

        }

        public bool RegisterUser(User register)
        {
            try
            {
                User user = new User()
                {
                    Id = AutoEncrementValues.AutoEncrementUser++,
                    UserName = register.UserName,
                    Age = register.Age,
                    RegisterationDate = DateTime.Now,
                    UsersFavorites = register.UsersFavorites
                };

                _users.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoginUser(string name, out User user)
        {
            user = _users.Where(c => c.UserName == name).SingleOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }

        public void Save()
        {
            if (!_serialization.SerializeToJsonFile(_filePath, _users))
            {
                Console.WriteLine("FILE NOT FOUND!!");
                Environment.Exit(0);
            }
        }

        public bool IsAlreadyExist(string name)
        {
            return _users.Any(u => u.UserName == name);
        }

        public List<Favorites> GetFavoriteByName(string name, User currentUser)
        {
            List<Favorites> favorites = currentUser.UsersFavorites.Where(f => f.Name == name).ToList();

            if (favorites.Count == 0)
                throw new FavoriteNotFoundException();

            return favorites;
        }

    }
}

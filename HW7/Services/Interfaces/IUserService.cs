using HW7.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Services.Interfaces
{
    internal interface IUserService
    {
        public bool RegisterUser(User register);
        public bool LoginUser(string name, out User user);
        public List<Favorites> GetFavoriteByName(string name, User currentUser);
        public void Save();
        public bool IsAlreadyExist(string name);
    }
}

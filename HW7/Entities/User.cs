using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime RegisterationDate { get; set; }

       public List<Favorites> UsersFavorites = new List<Favorites>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Entities
{
    internal class Favorites
    {
        public string Name { get; set; }
        List<Food> Foods { get; set; } = new List<Food>();
        List<Sport> Sports { get; set; } = new List<Sport>();

    }
}

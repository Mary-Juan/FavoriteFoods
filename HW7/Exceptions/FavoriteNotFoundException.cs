using HW7.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HW7.Exeptions
{
    internal class FavoriteNotFoundException : Exception
    {
        
        public override string Message => "No favorite found with this name";
        }

    
}

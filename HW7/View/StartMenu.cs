using HW7.Entities;
using HW7.Services.Interfaces;
using HW7.Services;
using HW7.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HW7.Exeptions;

namespace HW7.View
{
    internal class StartMenu
    {

        IUserService _userService;

        public StartMenu(string filePath)
        {
            _userService = new UserService(filePath);
        }

        public void RegisterMenu()
        {
            Console.WriteLine("Enter UserName:");
            string userName = Console.ReadLine();

            while(_userService.IsAlreadyExist(userName))
            {               
                    Console.WriteLine("name has already token in system. try another UserName.");    
                userName = Console.ReadLine();
            }

            Console.WriteLine("Enter Your Age:");
            int age = 0;

            while (!int.TryParse(Console.ReadLine(), out age))
            {
                EnterANumericValue();
            }

            List<Favorites> favorites = new List<Favorites>();
           bool toContinue = true;

            while(toContinue)
            {
                
                Favorites fav = ShowFavoriteListForm();
                favorites.Add(fav);
                Console.WriteLine("Do you want to add new favorite or no?('y' for yes and 'n' for no");
                string option = Console.ReadLine();

                switch(option) 
                {
                    case "y":break;
                    case "n": toContinue = false; break;
                    default:
                        Console.WriteLine("you didn't entered 'y' or 'n'");
                        toContinue = false; break;
                }
            }


            User user = new User()
            {
                UserName = userName,
                Age = age,
                UsersFavorites = favorites
            };          

            if (_userService.RegisterUser(user))
            {
                _userService.Save();
                Console.WriteLine("Successfull");
                Console.WriteLine("enter 's' to search and enter 'l' to logout");
                string opt = Console.ReadLine();

                if (opt == "s")
                    Search(user);

            }
            else
                Console.WriteLine("Not Successfull");
        }

        public void LoginMenu()
        {
            Console.WriteLine("Enter UserName:");
            string userName = Console.ReadLine();

            if(!_userService.LoginUser(userName, out User currentUser))
            {
                Console.WriteLine("Not Found");
           
            }
            else
            {
                ShowUserDetail(currentUser);
                bool toSearch = true;

                while (toSearch)
                {
                    Console.WriteLine("enter 's' to search and enter 'l' to logout");
                    string opt = Console.ReadLine();

                    switch(opt)
                    {
                        case "s": Search(currentUser); break;
                        case "l": toSearch = false; break;
                    }                                          
                }
            }
          
        }

        public Favorites ShowFavoriteListForm()
        {
            Console.WriteLine("what's your favorite?\n1.Food\n2.Sport");
            string favorite = Console.ReadLine();

            switch (favorite)
            {
                case "1":
                    {
                        Food food = ShowFoodForm();
                        return food;
                       
                    }

                case "2":
                    {
                        Sport sport = ShowSportForm();
                        return sport;
                      
                    }

                default:
                    {
                       
                        Console.WriteLine("You didn't enterd 1 or 2.");
                        return null;
                       
                    }
            }
        }

        public Food ShowFoodForm()
        {
            Console.WriteLine("Enter rhe food's name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter food's type: 1.Persian 2.Foreign");
            int foodTypenumber = 0;

            while(!int.TryParse(Console.ReadLine(), out foodTypenumber))
            {
                EnterANumericValue();
            }

            FoodTypes foodType = (FoodTypes)foodTypenumber;

            Food food = new Food()
            {
                Name = name,
                FoodTypes = foodType
            };

            return food;
        }

        public Sport ShowSportForm()
        {
            Console.WriteLine("enter the sport's name:");
            string name = Console.ReadLine();

            Console.WriteLine("Is it Professional?\n1.yes\n2.no");
            int isItProfessional = 0;

            while (!int.TryParse(Console.ReadLine(), out isItProfessional))
            {
                EnterANumericValue();
            }

            Sport sport = new Sport()
            {
                Name = name,
                IsProfessional = true ? isItProfessional == 1 : false
            };

            return sport;
        }

        private void EnterANumericValue()
        {
            Console.WriteLine("enter a numerical value.");
        }

        public void ShowUserDetail(User user)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Name : {user.UserName} \nAge: {user.Age}");
            Console.WriteLine("favorites:\n ----- ");
            Console.ForegroundColor = ConsoleColor.White;

            foreach (Favorites favorite in user.UsersFavorites)
            {
                ShowFavorite(favorite);
                Console.WriteLine("________");
            }

        }

        public void ShowFood(Food food)
        {
            Console.WriteLine($"title: {food.Name}\nFood's Type: {food.FoodTypes}");
        }

        public void ShowSport(Sport sport)
        {
            Console.WriteLine($"title: {sport.Name}\nIs it professional? {sport.IsProfessional}");
        }

        public void Search(User currentUser)
        {
            Console.WriteLine("enter a name of favorite to search:");
            string name = Console.ReadLine();

            try
            {
                List<Favorites> favorites = _userService.GetFavoriteByName(name, currentUser);

                foreach (var fav in favorites)
                {
                    ShowFavorite(fav);
                }
            }
            catch (FavoriteNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }            

        }

        public void ShowFavorite(Favorites favorite)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (favorite is Food)
            
                ShowFood((Food)favorite);
            
            else
                ShowSport((Sport)favorite);


            Console.ForegroundColor = ConsoleColor.White;
        }

        
    }
}

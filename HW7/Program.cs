using HW7.Services;
using HW7.View;

string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Users.txt");
StartMenu _startMenu = new StartMenu(_filePath);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Welcome!!!");
Console.ForegroundColor = ConsoleColor.White;

while (true)
{
    Console.WriteLine("1.Register 2.Login 3.Exit");
    string option = Console.ReadLine();

    switch (option)
    {
        case "1": _startMenu.RegisterMenu(); break;
        case "2": _startMenu.LoginMenu(); break;
        case "3": Environment.Exit(0); break;
    }
}
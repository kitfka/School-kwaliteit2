using LiteDB;
using Microsoft.AspNetCore.Identity;
using Webshop.Data;
using Webshop.Model;

namespace Webshop.Api.Data;

public static class DefaultDatabaseData
{
    /// <summary>
    /// A cli to configure the Webshop Api from commandline.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="NullReferenceException"></exception>
    public static void CommandLine(IServiceProvider serviceProvider)
    {
        IAuthService? auth = serviceProvider.GetService<IAuthService>();
        LiteDBService? dbService = serviceProvider.GetService<LiteDBService>();

        if (auth == null || dbService == null) 
        {
            Console.WriteLine("auth or dbService was missing. Fatel Error.");
            System.Environment.Exit(0);
            return; 
        }

        Console.WriteLine("Webshop Api Setup. use 'help' for commands, and 'quit' to exit");
        string x;

        while (true)
        {
            Console.Write("Webshop.Api (Cli): ");
            x = Console.ReadLine() ?? "";

            switch (x.ToLower())
            {
                case "r":
                case "register":
                    Console.WriteLine($"Password will be printed to terminal, a Console.Clean will be called after setting the user. Keep this in mind.");
                    Console.Write("Name(admin): ");
                    string name = Console.ReadLine() ?? "";
                    Console.Write("Email(admin@example.com): ");
                    string email = Console.ReadLine() ?? "";
                    Console.Write("Password (secret): ");
                    string password = Console.ReadLine() ?? "";
                    Console.Write("roles (Everyone,SecondRole): ");
                    string role = Console.ReadLine() ?? "";
                    if (password.Length <= 5)
                    {
                        Console.WriteLine("Bad password, try again");
                        break;
                    }


                    List<string> roles;
                    if (role.Contains(','))
                    {
                        roles = role.Split(',').ToList();
                    }
                    else
                    {
                        roles = [ role ];
                    }


                    auth.Register(new ApplicationUser()
                    {
                        Email = email,
                        Name = name,
                        Password = password,
                        Roles = roles.ToArray(),
                    });

                    Console.Clear();
                    Console.WriteLine($"{name} in user add list");
                    break;

                case "get-users":
                    Console.WriteLine("All users");

                    foreach (var user in dbService.data.GetCollection<ApplicationUser>().FindAll())
                    {
                        Console.WriteLine($"{user.Name}, {user.Id}, {string.Join(';', user.Roles)}, {user.Email}");
                    }
                    Console.WriteLine();
                    break;


                case "delete-user":
                    Console.Write("Enter user id: ");
                    string id = Console.ReadLine() ?? "";

                    if (Guid.TryParse(id, out Guid guidID))
                    {
                        var result = dbService.data.GetCollection<ApplicationUser>().FindById(guidID);
                        if (result == null)
                        {
                            Console.WriteLine("User not found!");
                            break;
                        }
                        bool r = dbService.data.GetCollection<ApplicationUser>().Delete(guidID);

                        Console.WriteLine($"Delete status returned {r}");
                    }
                    else
                    {
                        Console.WriteLine("Not a valid Guid!");
                    }
                    break;

                case "cls":
                case "clear":
                    Console.Clear();
                    break;

                case "q":
                case "quit":
                case "exit":
                    Console.Clear();
                    System.Environment.Exit(0);
                    break;

                case "c":
                case "continue":
                    Console.Clear();
                    return;

                case "h":
                case "help":
                    string helpMessage = @"
Remember to call the cli with ConnectionStrings:LiteDB to load the correct database!
./Webshop.Api init ConnectionStrings:LiteDB=path/To/Data.db

Command(alias): Description.

===== Command list =====

help(h):        Display this message.
register(r):    Register a new user.
get-users:      print list of all users.
delete-user:    Delete a user.
clear(cls):     Clear terminal.
exit(q):        Stop cli.
continue(c):    Continue execution of Webshop.Api. Will close Cli.

======= end list =======
";
                    Console.WriteLine(helpMessage);
                    break;
                default:
                    Console.WriteLine("Use 'help' for help");
                    break;
            }
        }
    }
}

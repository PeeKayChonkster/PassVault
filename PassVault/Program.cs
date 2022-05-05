using PassVault.Interfaces;
using PassVault.Services;
using PassVault.Models;
using System.Text.RegularExpressions;

internal static class Program
{
    private static readonly string[] _commandsInfo =
    {
        "help - list all commands",
        "clear - clear terminal window",
        "add (short id) (password) [(desctiprion)] - add new password",
        "list - list all passwords",
        "remove (short id) - remove password form database",
        "find (keyword) - search for passwords",
        "rereg - set new username and password",
        "exit - close program"
    };
    private static readonly string _configPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/passvault_config.dat";

    private static IInputOutput io = new ConsoleIO();
    private static bool _exiting = false;
    private static bool _autentificated = false;
    private static readonly char _starter = '>';
    private static Database db = new Database();

    public static void Main(string[] args)
    {
        io.WriteLine("Welcome to PassVault.");
        io.WriteLine("Your password is safe here.");

        while(!_exiting)
        {
            if(!_autentificated)
            {
                Autentificate();
            }
            else
            {
                io.Write(_starter);
                string? cmd = io.ReadLine();
                if (cmd != null)
                {
                    string[] words = cmd.Split(' ');
                    ExecCmd(words[0], words[1..^0]);
                }
            }

        }

        db.Dispose();
    }

    private static void ExecCmd(string cmd, params string[] args)
    {
        cmd = cmd.ToLower();
        Array.ForEach(args, (arg) => { arg = arg.ToLower(); });
        switch(cmd)
        {
            case "help":
                {
                    foreach(string command in _commandsInfo)
                    {
                        io.WriteLine(command);
                    }
                    break;
                }
            case "clear":
                {
                    io.Clear();
                    break;
                }
            case "add":
                {
                    if (args.Length < 2 || args[0] == "" || args[1] == "")
                    {
                        io.WriteLine("Wrong input. Enter \"help\" to list all commands");
                        return;
                    }
                    string desc = args.Length > 2? String.Join(" ", args[2..^0]) : "";
                    db.Add(new Password(args[0], args[1], desc));
                    io.WriteLine("Added Password " + args[0]);
                    break;
                }
            case "remove":
                {
                    if (args.Length == 0 || args[0] == "")
                    {
                        io.WriteLine("Wrong input. Enter \"help\" to list all commands");
                        return;
                    }
                    if (db.Delete(args[0]))
                    {
                        io.WriteLine("Deleted password " + args[0]);
                    }
                    else
                    {
                        io.WriteLine("Didn't find password " + args[0]);
                    }
                    break;
                }
            case "list":
                {
                    foreach(Password password in db.Passwords)
                    {
                        io.WriteLine("-------------------");
                        io.WriteLine(password.ToString());
                        io.WriteLine("-------------------");
                        io.WriteLine("");
                    }
                    break;
                }
            case "find":
                {
                    if (args.Length == 0 || args[0] == "")
                    {
                        io.WriteLine("Wrong input. Enter \"help\" to list all commands");
                        return;
                    }
                    Regex reg = new Regex($"{args[0]}");
                    var passwords = from password in db.Passwords
                                           where reg.IsMatch(password.Id) || reg.IsMatch(password.Description)
                                           select password;
                    io.WriteLine("Search results:\n");
                    foreach(var password in passwords)
                    {
                        io.WriteLine("-------------------");
                        io.WriteLine(password.ToString());
                        io.WriteLine("-------------------");
                        io.WriteLine("");
                    }
                    break;
                }
            case "rereg":
                {
                    io.Write("Enter new username: ");
                    string username = io.ReadLine();
                    io.Write("Enter new password: ");
                    string password = io.ReadLine();
                    string[] lines = { Tools.EncodeString(username), Tools.EncodeString(password) };
                    System.IO.File.WriteAllLines(_configPath, lines);
                    io.WriteLine("Account was changed. Welcome, " + username);
                    break;
                }
            case "exit":
                {
                    Exit();
                    break;
                }
            default:
                {
                    io.WriteLine("Enter \"help\" to list all available commands");
                    break;
                }
        }
    }

    private static void Exit()
    {
        _exiting = true;
        io.WriteLine("Goodbye!");
    }

    private static char ToLower(this char c)
    {
        return c.ToString().ToLower()[0];
    }

    private static void Autentificate()
    {
        if (System.IO.File.Exists(_configPath))
        {
            string[] lines = System.IO.File.ReadAllLines(_configPath);
            if (lines.Length < 2)
            {
                io.WriteLine("Config file is damaged. Do you want to create new account? (y/n)");
                io.Write(_starter);
                char c = io.ReadKey().ToLower();
                if(c.ToLower() == 'n')
                {
                    Exit();
                }
                else
                {
                    CreateAccount();
                }
            }
            else
            {
                io.Write("Username: ");
                string username = Tools.DecodeString(io.ReadLine());
                io.Write("Password: ");
                string password = Tools.DecodeString(io.ReadLine());
                if(username == lines[0] && password == lines[1])
                {
                    io.WriteLine("Autentification was successful. Welcome, " + username);
                    _autentificated = true;
                }
                else if(username != lines[0])
                {
                    io.WriteLine("Username is incorrect");
                }
                else
                {
                    io.WriteLine("Password is incorrent");
                }
            }
        }
        else
        {
            CreateAccount();
        }
    }

    private static void CreateAccount()
    {
        io.WriteLine("Create new account");
        io.Write("Username: ");
        string username = io.ReadLine();
        io.Write("Password: ");
        string password = io.ReadLine();
        string[] lines = { Tools.EncodeString(username), Tools.EncodeString(password) };
        System.IO.File.WriteAllLines(_configPath, lines);
        io.WriteLine("Account created. Welcome, " + username);
        _autentificated = true;
    }
}
using PassVault.Interfaces;
using PassVault.Services;
using PassVault.Models;

internal static class Program
{
    private static readonly string[] _commandsInfo =
    {
        "help - list all commands",
        "clear - clear terminal window",
        "add (short id) (password) [(desctiprion)] - add new password",
        "list - list all passwords",
        "remove (short id) - remove password form database",
        "exit - close program"
    };

    private static IInputOutput io = new ConsoleIO();
    private static bool _exiting = false;
    private static readonly char _starter = '>';
    private static Database db = new Database();

    public static void Main(string[] args)
    {
        io.WriteLine("Welcome to PassVault.");
        io.WriteLine("Your password is safe here.");

        while(!_exiting)
        {
            io.Write(_starter);
            string? cmd = io.ReadLine();
            if(cmd != null)
            {
                string[] words = cmd.Split(' ');
                ExecCmd(words[0], words[1..^0]);
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
                    if (args.Length == 0 || args[0] == "" || args[1] == "") return;
                    string desc = args.Length > 2? args[2] : "";
                    db.Add(new Password(args[0], args[1], desc));
                    io.WriteLine("Added Password " + args[0]);
                    break;
                }
            case "remove":
                {
                    if (args.Length == 0 || args[0] == "") return;
                    if(db.Delete(args[0]))
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
                        io.WriteLine(password.ToString());
                    }
                    break;
                }
            case "exit":
                {
                    _exiting = true;
                    io.WriteLine("Goodbye!");
                    break;
                }
            default:
                {
                    io.WriteLine("Enter \"help\" to list all available commands");
                    break;
                }
        }
    }
}
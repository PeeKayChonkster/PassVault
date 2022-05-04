using PassVault.Interfaces;
using PassVault.Services;

internal static class Program
{
    private static readonly string[] _commandsInfo =
    {
        "help - list all commands",
        "clear - clear terminal window",
        "exit - close program"
    };

    private static IInputOutput io = new ConsoleIO();
    private static bool _exiting = false;
    private static readonly char _starter = '>';

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
            case "exit":
                {
                    _exiting = true;
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
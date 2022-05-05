using PassVault.Interfaces;

namespace PassVault.Services
{
    internal class ConsoleIO : IInputOutput
    {
        public void Clear()
        {
            Console.Clear();
        }

        public int Read()
        {
            return Console.Read();
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string line)
        {
            Console.Write(line);
        }

        public void Write(char ch)
        {
            Console.Write(ch);
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}

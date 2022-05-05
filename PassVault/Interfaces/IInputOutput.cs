
namespace PassVault.Interfaces
{
    internal interface IInputOutput
    {
        void Write(string line);
        void Write(char ch);
        void WriteLine(string line);
        string? ReadLine();
        int Read();
        char ReadKey();
        void Clear();
    }
}


namespace PassVault.Interfaces
{
    internal interface IInputOutput
    {
        void Write(string line);
        void WriteLine(string line);
        string? ReadLine();
        void Clear();
    }
}

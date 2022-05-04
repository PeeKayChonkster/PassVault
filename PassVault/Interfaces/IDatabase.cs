using PassVault.Models;

namespace PassVault.Interfaces
{
    internal interface IDatabase
    {
        List<Password> Passwords { get; }
        void Add(Password password);
        void Delete(string id);
    }
}

using PassVault.Models;

namespace PassVault.Interfaces
{
    internal interface IDatabase
    {
        List<Password> Passwords { get; }
        void Add(Password password);
        bool Delete(string id);
        void SaveChanges();
    }
}

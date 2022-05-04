using PassVault.Interfaces;
using PassVault.Models;

namespace PassVault.Services
{
    internal class Database : IDatabase
    {
        private static readonly string _fileName = "vault_data.dat";
        public Database()
        {
            Passwords = new List<Password>();
            ParseFile();
        }

        public List<Password> Passwords { get; private set; }

        private void ParseFile()
        {
            if (System.IO.File.Exists("./" + _fileName))
            {
                string[] lines = System.IO.File.ReadAllLines("./" + _fileName);
                foreach(string line in lines)
                {
                    string[] words = line.Split(',');
                    Passwords.Add(new Password(words[0], words[1], words[2]));
                }
            }
        }

        public void Add(Password password)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

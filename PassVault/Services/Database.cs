using PassVault.Interfaces;
using PassVault.Models;

namespace PassVault.Services
{
    internal class Database : IDatabase, IDisposable
    {
        private bool _disposed = false;
        private static readonly string _filePath = Tools.FolderPath + "vault_data.dat";
        public Database()
        {
            Passwords = new List<Password>();
            ParseFile();
        }

        public List<Password> Passwords { get; private set; }

        private void ParseFile()
        {
            if (System.IO.File.Exists(_filePath))
            {
                string[] lines = System.IO.File.ReadAllLines(_filePath);
                foreach(string line in lines)
                {
                    string[] words = line.Split(',');
                    Password password = Tools.DecodePassword(new Password(words[0], words[1], words[2]));
                    Passwords.Add(password);
                }
            }
        }

        public void Add(Password password)
        {
            if(!Passwords.Contains(password))
            {
                Passwords.Add(password);
            }
        }

        public bool Delete(string id)
        {
            Password? password = (from pass in Passwords
                                where pass.Id == id
                                select pass).FirstOrDefault();
            if(password != null)
            {
                Passwords.Remove(password);
                return true;
            }
            return false;
        }

        public void SaveChanges()
        {
            List<string> lines = new List<string>();
            foreach(Password password in Passwords)
            {
                lines.Add(password.ToCsvEncoded());
            }
            if(!System.IO.Directory.Exists(Tools.FolderPath)) System.IO.Directory.CreateDirectory(Tools.FolderPath);
            System.IO.File.WriteAllLines(_filePath, lines.ToArray());
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        private void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            if (disposing)
            {
                SaveChanges();
            }

            _disposed = true;
        }
    }
}

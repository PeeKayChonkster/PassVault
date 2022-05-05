using PassVault.Interfaces;
using PassVault.Models;

namespace PassVault.Services
{
    internal class Database : IDatabase, IDisposable
    {
        private bool _disposed = false;
        private static readonly string _folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/PassVault/";
        private static readonly string _filePath = _folderPath + "vault_data.dat";
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
                    Passwords.Add(new Password(words[0], words[1], words[2]));
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
                lines.Add(password.ToCsv());
            }
            if(!System.IO.Directory.Exists(_folderPath)) System.IO.Directory.CreateDirectory(_folderPath);
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

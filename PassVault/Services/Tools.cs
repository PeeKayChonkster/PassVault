using System.Text;
using PassVault.Models;

namespace PassVault.Services
{
    internal static class Tools
    {
        private static readonly int key1 = 8; //even
        private static readonly int key2 = 32; //even
        public static readonly string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/PassVault/";

        public static char EncodeChar(in char c)
        {
            int ic = (int)c;
            int key = ic % 2 == 0 ? key1 : key2;
            return ic + key < 255 ? (char)(ic + key) : (char)(ic - key);
        }

        public static char DecodeChar(in char c)
        {
            int ic = (int)c;
            int key = ic % 2 == 0 ? key1 : key2;
            return ic + 2 * key < 255 ? (char)(ic - key) : (char)(ic + key);
        }

        public static string EncodeString(in string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in str)
            {
                sb.Append(EncodeChar(c));
            }
            return sb.ToString();
        }
        public static string DecodeString(in string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                sb.Append(DecodeChar(c));
            }
            return sb.ToString();
        }

        public static Password EncodePassword(in Password password)
        {
            string id = EncodeString(password.Id);
            string pass = EncodeString(password.Pass);
            string description = EncodeString(password.Description);
            return new Password(id, pass, description);
        }

        public static Password DecodePassword(in Password password)
        {
            string id = DecodeString(password.Id);
            string pass = DecodeString(password.Pass);
            string description = DecodeString(password.Description);
            return new Password(id, pass, description);
        }

        public static char ToLower(this char c)
        {
            return c.ToString().ToLower()[0];
        }
    }
}

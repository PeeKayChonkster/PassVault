using System.Text;

namespace PassVault.Models
{
    internal record Password
    {
        public string Id { get; set; }
        public string Pass { get; set; }
        public string Description { get; set; }

        public Password(string id, string pass, string description = "")
        {
            this.Id = id;
            this.Pass = pass;
            this.Description = description;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id : {Id}\n");
            sb.Append($"Password : {Pass}\n");
            sb.Append($"Description : {Description}");

            return sb.ToString();
        }

        public string ToCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id.ToString() + ',');
            sb.Append(Pass.ToString() + ',');
            sb.Append(Description.ToString() );

            return sb.ToString();
        }
    }
}

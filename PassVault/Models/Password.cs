using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

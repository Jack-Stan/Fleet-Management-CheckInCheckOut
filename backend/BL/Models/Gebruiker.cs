using System;
using System.Collections.Generic;

namespace BL.Models {
    public class Gebruiker {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string WachtwoordHash { get; set; } = string.Empty;

        public Gebruiker(string email, string wachtwoordHash) {
            Email = email;
            WachtwoordHash = wachtwoordHash;
        }

        public Gebruiker() { }
    }
}

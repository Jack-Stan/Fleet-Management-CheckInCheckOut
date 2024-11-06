namespace BL.Models {
    public class Bestuurder {
        public int Id { get; set; } 
        public string Naam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string RijbewijsNummer { get; set; }
        public string RijbewijsType { get; set; }
        public string? Bedrijfsnaam { get; set; } 
        public string? BedrijfsBTW { get; set; }

        public List<Reservering> Reserveringen { get; set; } = new List<Reservering>();

        public Bestuurder(string naam, DateTime geboortedatum, string rijbewijsNummer, string rijbewijsType, string? bedrijfsnaam = null, string? bedrijfsBTW = null) {
            Naam = naam;
            Geboortedatum = geboortedatum;
            RijbewijsNummer = rijbewijsNummer;
            RijbewijsType = rijbewijsType;
            Bedrijfsnaam = bedrijfsnaam;
            BedrijfsBTW = bedrijfsBTW;
        }

        public Bestuurder() { }
    }
}

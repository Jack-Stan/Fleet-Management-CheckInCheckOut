using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Voertuig
    {
        public Voertuig(string merk, string model, string chasisNummer, string kenteken, string voertuigType, string brandstofType, string kleur, int aantalZitplaatsen, int aantalDeuren)
        {
            Merk = merk;
            Model = model;
            ChasisNummer = chasisNummer;
            Kenteken = kenteken;
            VoertuigType = voertuigType;
            BrandstofType = brandstofType;
            Kleur = kleur;
            AantalZitplaatsen = aantalZitplaatsen;
            AantalDeuren = aantalDeuren;
        }

        public Voertuig(int id, string merk, string model, string chasisNummer, string kenteken, string voertuigType, string brandstofType, string kleur, int aantalZitplaatsen, int aantalDeuren)
        {
            Id = id;
            Merk = merk;
            Model = model;
            ChasisNummer = chasisNummer;
            Kenteken = kenteken;
            VoertuigType = voertuigType;
            BrandstofType = brandstofType;
            Kleur = kleur;
            AantalZitplaatsen = aantalZitplaatsen;
            AantalDeuren = aantalDeuren;
        }

        public Voertuig()
        {   
        }

        public int Id { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public string ChasisNummer { get; set; }
        public string Kenteken { get; set; }
        public string VoertuigType { get; set; }
        public string BrandstofType { get; set; }
        public string Kleur { get; set; }
        public int AantalZitplaatsen { get; set; }
        public int AantalDeuren { get; set; }

        public List<Reservering> Reserveringen { get; set; } = new List<Reservering>();
    }
}

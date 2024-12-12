using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO.Input
{
    public class BestuurderInputDTO
    {
        public string Naam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string RijbewijsNummer { get; set; }
        public string RijbewijsType { get; set; }
        public string Bedrijfsnaam { get; set; }
        public string BedrijfsBTW { get; set; }
    }
}

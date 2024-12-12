using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO.Output
{
    public class BestuurderOutputDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public DateTime Geboortedatum { get; set; }
        public string RijbewijsNummer { get; set; } = string.Empty;
        public string RijbewijsType { get; set; } = string.Empty;
        public string Bedrijfsnaam { get; set; } = string.Empty;
        public string BedrijfsBTW { get; set; } = string.Empty;
    }
}

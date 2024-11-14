using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO.Input
{
    public class VoertuigInputDTO
    {
        public string Kenteken { get; set; } = string.Empty;
        public string Merk { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Bouwjaar { get; set; }
        public string Kleur { get; set; } = string.Empty;
    }
}

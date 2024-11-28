using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Models
{
    public class CheckInSchade
       {
        public int Id { get; set; }
        public string Reserveringsnummer { get; set; }
        public string FotoPad { get; set; }
        public string SchadeType { get; set; }
        public string Opmerkingen { get; set; }
        public DateTime Datum { get; set; }
    }
}
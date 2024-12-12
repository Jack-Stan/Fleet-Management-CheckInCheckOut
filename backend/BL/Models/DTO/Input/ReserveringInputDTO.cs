using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO.Input
{
    public class ReserveringInputDTO
    {
        public int BestuurderId { get; set; }
        public int VoertuigId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

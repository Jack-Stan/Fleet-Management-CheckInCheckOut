using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO.Output
{
    public class ReserveringOutputDTO
    {
        public int Id { get; set; }
        public int BestuurderId { get; set; }
        public int VoertuigId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CheckOutState { get; set; } = string.Empty;
        public List<string> CheckOutPictures { get; set; } = [];
        public string CheckInState { get; set; } = string.Empty;
        public List<string> CheckInPictures { get; set; } = [];
    }
}

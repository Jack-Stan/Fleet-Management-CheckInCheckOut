using System;
using System.Collections.Generic;

namespace BL.Models {
    public class Reservering {
        public int Id { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public string? CheckOutState { get; set; }
        public string? CheckInState { get; set; }
        public List<string>? CheckOutPictures { get; set; }
        public List<string>? CheckInPictures { get; set; }

        public int VoertuigId { get; set; } 
        public int BestuurderId { get; set; } 

        public Reservering(DateTime startDatum, DateTime eindDatum, int voertuigId, int bestuurderId, string? checkOutState = null, string? checkInState = null) {
            StartDatum = startDatum;
            EindDatum = eindDatum;
            VoertuigId = voertuigId;
            BestuurderId = bestuurderId;
            CheckOutState = checkOutState;
            CheckInState = checkInState;
            CheckOutPictures = new List<string>();
            CheckInPictures = new List<string>();
        }

        public Reservering() { }
    }
}

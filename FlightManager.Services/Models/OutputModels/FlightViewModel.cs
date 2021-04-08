using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    public class FlightViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string PlaneType { get; set; }
        public int UniquePlaneNumber { get; set; }
        public DateTime DateTimeTakeOff { get; set; }
        public TimeSpan Duration { get; set; }
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}

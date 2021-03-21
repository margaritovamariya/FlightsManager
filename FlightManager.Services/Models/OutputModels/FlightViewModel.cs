using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    public class FlightViewModel
    {
        public int UniquePlaneNumber { get; set; }
        public DateTime DateTimeTakeOff { get; set; }
        public TimeSpan Duration { get; set; }
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}

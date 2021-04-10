using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    public class FlightViewModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PlaneType { get; set; }
        public int UniquePlaneNumber { get; set; }
        public string PilotName { get; set; }
        public DateTime DateTimeTakeOff { get; set; }
        public DateTime DateTimeLanding { get; set; }
        public TimeSpan Duration { get; set; }
        public int PassengerCapacity { get; set; }
        public int BusinessClassCapacity { get; set; }
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}

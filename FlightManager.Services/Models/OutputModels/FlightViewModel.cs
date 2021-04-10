using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// FlightsModel
    /// </summary>
    public class FlightViewModel
    {
        /// <summary>
        /// gets,sets Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// gets,sets From
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// gets,sets To
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// gets,sets PlaneType
        /// </summary>
        public string PlaneType { get; set; }
        /// <summary>
        /// gets,sets UniquePlaneNumber
        /// </summary>
        public int UniquePlaneNumber { get; set; }
        /// <summary>
        /// gets,sets PilotName
        /// </summary>
        public string PilotName { get; set; }
        /// <summary>
        /// gets,sets DateTimeTakeOff
        /// </summary>
        public DateTime DateTimeTakeOff { get; set; }
        /// <summary>
        /// gets,sets DateTimeLanding
        /// </summary>
        public DateTime DateTimeLanding { get; set; }
        /// <summary>
        /// gets,sets Duration
        /// </summary>
        public TimeSpan Duration { get; set; }
        public int PassengerCapacity { get; set; }
        /// <summary>
        /// gets,sets BusinessClassCapacity
        /// </summary>
        public int BusinessClassCapacity { get; set; }
        /// <summary>
        /// gets,sets List of Reservations
        /// </summary>
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}

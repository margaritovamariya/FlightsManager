using FlightManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class Flight
    {
        public Flight()
        {
            this.ReservationFlights = new HashSet<ReservationFlight>();
        }

        //PlaneId
        [Required]
        public int Id { get; set; }

        //DestinationFrom
        [Required]
        [MinLength(GlobalConstants.FromMinLength)]
        public string From { get; set; }

        //DestinationTo
        [Required]
        [MinLength(GlobalConstants.ToMinLength)]
        public string To { get; set; }

        //DepartureTime
        [Required]
        public DateTime DateTimeTakeOff { get; set; }

        //ArrivalTime
        [Required]
        public DateTime DateTimeLanding { get; set; }

        public string PlaneType { get; set; }

        [Required]
        [MinLength(GlobalConstants.UniquePlaneNumberMinLength)]
        public int UniquePlaneNumber { get; set; }

        public string PilotName { get; set; }

        [Required]
        [Range(GlobalConstants.PassengersCapacityMinLength, GlobalConstants.PassengersCapacityMaxLength)]
        public int PassengersCapacity { get; set; }

        [Required]
        [Range(GlobalConstants.BusinessClassMinCapacity, GlobalConstants.BusinessClassMaxCapacity)]
        public int BusinessClassCapacity { get; set; }

        //ReservationFlight relation
        public virtual ICollection<ReservationFlight> ReservationFlights { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public interface IReservationService
    {
        //Create
        void Create(string firstName, string secondName, string familyName, long pin, string telephoneNumber, string nationality, string ticketType);

        //Read
        IEnumerable<ReservationViewModel> GetFlightReservations(int flightId);

        //Update
        IEnumerable<ReservationViewModel> UpdateReservation(int reservationId);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public interface IReservationService
    {
        //Create
        void Create(string firstName, string secondName, string familyName, long pin, string email, 
                    string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber);

        //Read
        IEnumerable<ReservationViewModel> GetFlightReservations(int uniquePlaneNumber);

        //Update
        void UpdateReservation(int reservationId, string firstName, string secondName, string familyName, long pin,
                                string telephoneNumber, string nationality, string ticketType);
    }
}

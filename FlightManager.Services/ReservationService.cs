using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private FlightManagerDbContext dbContext;

        public ReservationService(FlightManagerDbContext context)
        {
            this.dbContext = context;
        }

        //CHECK IF THE FLIGHT EXIST
        public void Create(string firstName, string secondName, string familyName, long pin, string telephoneNumber,
            string nationality, string ticketType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationViewModel> GetFlightReservations(int flightId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationViewModel> UpdateReservation(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}

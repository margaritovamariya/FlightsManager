using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Models;
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

        public void Create(string firstName, string secondName, string familyName, long pin, string telephoneNumber,
            string nationality, string ticketType, int uniquePlaneNumber)
        {
            var flight = dbContext.Flights.FirstOrDefault(x => x.UniquePlaneNumber == uniquePlaneNumber);

            if (ticketType == GlobalConstants.TicketTypeRegular)
            {
                if (flight.PassengersCapacity > 0)
                {
                    var reservation = AddReservation(firstName, secondName, familyName, pin, telephoneNumber, nationality, ticketType, uniquePlaneNumber, flight);
                    flight.PassengersCapacity -= 1;
                    this.dbContext.Reservations.Add(reservation);
                }
                else
                {
                    throw new ArgumentException(ExceptionMessages.NotEnoughAmountOfRegularTickets);
                }
                
            }
            else if (ticketType == GlobalConstants.TicketTypeBusinessClass )
            {
                if (flight.BusinessClassCapacity > 0)
                {
                    var reservation = AddReservation(firstName, secondName, familyName, pin, telephoneNumber, nationality, ticketType, uniquePlaneNumber, flight);
                    flight.BusinessClassCapacity -= 1;
                    this.dbContext.Reservations.Add(reservation);
                }
                else
                {
                    throw new ArgumentException(ExceptionMessages.NotEnoughAmountOfBusinessClassTickets);
                }
                
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidTicketType);
            }

            this.dbContext.SaveChanges();
        }

        private Reservation AddReservation(string firstName, string secondName, string familyName, long pin, string telephoneNumber,
                                    string nationality, string ticketType, int uniquePlaneNumber, Flight flight)
        {
            var reservation = new Reservation();

            if (flight.UniquePlaneNumber == uniquePlaneNumber)
            {

                reservation.FirstName = firstName;
                reservation.SecondName = secondName;
                reservation.FamilyName = familyName;
                reservation.PIN = pin;
                reservation.TelephoneNumber = telephoneNumber;
                reservation.Nationality = nationality;
                reservation.FlightId = flight.Id;


                //Check ticket type
                var ticketTypeEntity = this.dbContext.TicketTypes.FirstOrDefault(x => x.Name == ticketType);

                if (ticketTypeEntity == null)
                {
                    ticketTypeEntity = new TicketType()
                    {
                        Name = ticketType
                    };
                }

                reservation.TicketType = ticketTypeEntity;

            }
            else
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidFlight);
            }

            return reservation;
        }

        public IEnumerable<ReservationViewModel> GetFlightReservations(int uniquePlaneNumber)
        {
            var reservations = this.dbContext.Reservations
                .Where(x => x.Flight.UniquePlaneNumber == uniquePlaneNumber)
                .Select(r => new ReservationViewModel()
                {
                    FirstName = r.FirstName,
                    SecondName = r.SecondName,
                    FamilyName = r.FamilyName,
                    PIN = r.PIN,
                    TelephoneNumber = r.TelephoneNumber,
                    Nationality = r.Nationality,
                    TicketType = r.TicketType.Name
                })
                .ToList();

            return reservations;
        }

        public void UpdateReservation(int reservationId, string firstName, string secondName, string familyName, long pin, 
                                      string telephoneNumber, string nationality, string ticketType)
        {
            var reservation = this.dbContext.Reservations.FirstOrDefault(x => x.Id == reservationId);

            if (reservation != null)
            {
                reservation.FirstName = firstName;
                reservation.SecondName = secondName;
                reservation.FamilyName = familyName;
                reservation.PIN = pin;
                reservation.TelephoneNumber = telephoneNumber;
                reservation.Nationality = nationality;
                reservation.TicketType.Name = ticketType;
            }
            else
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidReservation);
            }

            this.dbContext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public class FlightService : IFlightService
    {
        private FlightManagerDbContext dbContext;

        public FlightService(FlightManagerDbContext context)
        {
            this.dbContext = context;
        }
        
        public void Create(string from, string to, DateTime dateTimeTakeOff, DateTime dateTimeLanding, string planeType,
            int uniquePlaneNumber, string pilotName, int passengersCapacity, int businessClassCapacity)
        {
            var flight = new Flight();

            //From to input
            if (from != to)
            {
                flight.From = from;
                flight.To = to;
            }

            //DateTime input
            if (dateTimeTakeOff < dateTimeLanding)
            {
                flight.DateTimeTakeOff = dateTimeTakeOff;
                flight.DateTimeLanding = dateTimeLanding;
            }

            //PlaneType input
            flight.PlaneType = planeType;

            //Unique number input
            if (!dbContext.Flights.Any(x => x.UniquePlaneNumber == uniquePlaneNumber))
            {
                flight.UniquePlaneNumber = uniquePlaneNumber;
            }

            //Pilot name input
            flight.PilotName = pilotName;

            //Passenger capacity input
            if (passengersCapacity >= 5 && passengersCapacity <= 1000)
            {
                flight.PassengersCapacity = passengersCapacity;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPassengerAmount);
            }

            //BusinessClass capacity input
            if (businessClassCapacity >= 5 && businessClassCapacity <= 1000)
            {
                flight.BusinessClassCapacity = businessClassCapacity;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidBusinessAmount);
            }

            //Add the property to the Db and save changes
            this.dbContext.Flights.Add(flight);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<FlightViewModel> GetExactFlight(string from, string to, DateTime dateTimeTakeOff,
                                                          DateTime dateTimeLanding, string planeType, int uniquePlaneNumber)
        {
            var flights = dbContext.Flights
                .Where(f => f.From == from 
                            && f.To == to 
                            && f.DateTimeTakeOff == dateTimeTakeOff
                            && f.DateTimeLanding == dateTimeLanding 
                            && f.PlaneType == planeType
                            && f.UniquePlaneNumber == uniquePlaneNumber)
                .Select(MapToFlightViewModel())
                .ToList();

            return flights;
        }

        public IEnumerable<FlightViewModel> GetAllFlights()
        {
            var flights = dbContext.Flights
                .Select(MapToFlightViewModel())
                .ToList();

            return flights;
        }

        private static Expression<Func<Flight, FlightViewModel>> MapToFlightViewModel()
        {
            return x => new FlightViewModel()
            {
                UniquePlaneNumber = x.UniquePlaneNumber,
                DateTimeTakeOff = x.DateTimeTakeOff,
                Duration = x.DateTimeLanding - x.DateTimeTakeOff,
                Reservations = x.Reservations.Select(r => new ReservationViewModel()
                {
                    FirstName = r.FirstName,
                    SecondName = r.SecondName,
                    FamilyName = r.FamilyName,
                    PIN = r.PIN,
                    Nationality = r.Nationality,
                    TelephoneNumber = r.TelephoneNumber,
                    TicketType = r.TicketType.Name
                }).ToList()
            };
        }

        public void UpdateFlight(string from, string to, DateTime dateTimeTakeOff, DateTime dateTimeLanding, string planeType,
                                int uniquePlaneNumber, string pilotName, int passengersCapacity, int businessClassCapacity)
        {
            if (this.dbContext.Flights.Any(x=>x.UniquePlaneNumber == uniquePlaneNumber) == true)
            {
                var flight = this.dbContext.Flights.FirstOrDefault(f => f.UniquePlaneNumber == uniquePlaneNumber);
                flight.From = from;
                flight.To = to;
                flight.DateTimeTakeOff = dateTimeTakeOff;
                flight.DateTimeLanding = dateTimeLanding;
                flight.PlaneType = planeType;
                flight.PilotName = pilotName;
                flight.PassengersCapacity = passengersCapacity;
                flight.BusinessClassCapacity = businessClassCapacity;
            }
            else
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidUniqueNumberPlane);
            }

            this.dbContext.SaveChanges();
        }

        public void DeleteFlight(string from, string to, DateTime dateTimeTakeOff, DateTime dateTimeLanding, string planeType,
            int uniquePlaneNumber)
        {
            //Take the flight which we want to remove
            var flightToRemove = this.dbContext.Flights
                .FirstOrDefault(f => f.From == from && f.To == to && f.DateTimeTakeOff == dateTimeTakeOff
                                     && f.DateTimeLanding == dateTimeLanding && f.PlaneType == planeType &&
                                     f.UniquePlaneNumber == uniquePlaneNumber);

            if (flightToRemove != null)
            {
                //Take the reservations for this flight
                var reservationsToDelete = this.dbContext.Reservations.Where(x => x.FlightId == flightToRemove.Id);

                //Remove all reservations
                foreach (var reservation in reservationsToDelete)
                {
                    this.dbContext.Remove(reservation);
                }

                this.dbContext.Flights.Remove(flightToRemove);
                this.dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidFlight);
            }
        }
    }
}

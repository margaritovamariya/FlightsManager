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
    /// <summary>
    /// Клас който рабори с базата данни за полети
    /// </summary>
    public class FlightService : IFlightService
    {
        private  readonly FlightManagerDbContext dbContext;

        /// <summary>
        /// Сетва базата данни
        /// </summary>
        /// <param name="context"></param>
        public FlightService(FlightManagerDbContext context)
        {
            this.dbContext = context;
        }
        
        /// <summary>
        /// Създава полет
        /// </summary>
        /// <param name="model"></param>
        public void Create(FlightViewModel model)
        {
            var flight = new Flight();

            //From to input
            if (model.From != model.To)
            {
                flight.From = model.From;
                flight.To = model.To;
            }

            //DateTime input
            if (model.DateTimeTakeOff < model.DateTimeLanding)
            {
                flight.DateTimeTakeOff = model.DateTimeTakeOff;
                flight.DateTimeLanding = model.DateTimeLanding;
            }

            //PlaneType input
            flight.PlaneType = model.PlaneType;

            //Unique number input
            if (!dbContext.Flights.Any(x => x.UniquePlaneNumber == model.UniquePlaneNumber))
            {
                flight.UniquePlaneNumber = model.UniquePlaneNumber;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.PlaneWithThisUniqueNumberExist);
            }

            //Pilot name input
            flight.PilotName = model.PilotName;

            //Passenger capacity input
            if (model.PassengerCapacity >= 5 && model.PassengerCapacity <= 1000)
            {
                flight.PassengersCapacity = model.PassengerCapacity;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPassengerAmount);
            }

            //BusinessClass capacity input
            if (model.BusinessClassCapacity >= 5 && model.BusinessClassCapacity <= 1000)
            {
                flight.BusinessClassCapacity = model.BusinessClassCapacity;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidBusinessAmount);
            }

            //Add the property to the Db and save changes
            this.dbContext.Flights.Add(flight);
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Взима определен полет по дадените параметри
        /// </summary>
        /// <returns>полета с дадения номер</returns>
        
        public IEnumerable<FlightViewModel> GetExactFlight(int uniquePlaneNumber)
        {
            var flights = dbContext.Flights
                .Where(f => f.UniquePlaneNumber == uniquePlaneNumber)
                .Select(MapToFlightViewModel())
                .ToList();

            return flights;
        }

        /// <summary>
        /// Взимат се всички полети
        /// </summary>
        /// <returns> всички полети </returns>
        /// 

        public IEnumerable<FlightViewModel> GetAllFlights()
        {
            var flights = dbContext.Flights
                .Select(MapToFlightViewModel())
                .ToList();

            return flights;
        }

        /// <summary>
        /// Функция, която се извиква от горния метод
        /// </summary>
        /// <returns>лист от flightviewmodel </returns>
       
        private static Expression<Func<Flight, FlightViewModel>> MapToFlightViewModel()
        {
            return x => new FlightViewModel()
            {
                Id = x.Id,
                From = x.From,
                To = x.To,
                PlaneType = x.PlaneType,
                UniquePlaneNumber = x.UniquePlaneNumber,
                DateTimeTakeOff = x.DateTimeTakeOff,
                Duration = x.DateTimeLanding - x.DateTimeTakeOff,
                PassengerCapacity = x.PassengersCapacity,
                BusinessClassCapacity = x.BusinessClassCapacity,
                Reservations = x.Reservations.Select(r => new ReservationViewModel()
                {
                    FirstName = r.FirstName,
                    SecondName = r.SecondName,
                    FamilyName = r.FamilyName,
                    PIN = r.PIN,
                    Email = r.Email,
                    Nationality = r.Nationality,
                    TelephoneNumber = r.TelephoneNumber,
                    TicketType = r.TicketType.Name
                }).ToList()
            };
        }
    }
}

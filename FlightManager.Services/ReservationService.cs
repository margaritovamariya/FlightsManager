using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private const int PageSize = 25;
        private readonly FlightManagerDbContext dbContext;

        public ReservationService(FlightManagerDbContext context)
        {
            this.dbContext = context;
        }
        
        /// <summary>
        /// Създава резервация
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="secondName"></param>
        /// <param name="familyName"></param>
        /// <param name="pin"></param>
        /// <param name="email"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="nationality"></param>
        /// <param name="ticketType"></param>
        /// <param name="uniquePlaneNumber"></param>
        public void Create(ReservationListViewModel reservationView, int uniquePlaneNumber)
        {
            var flight = dbContext.Flights.FirstOrDefault(x => x.UniquePlaneNumber == uniquePlaneNumber);

            foreach(var Reservations in reservationView.reservations)
            {
                if (Reservations.TicketType == GlobalConstants.TicketTypeRegular)
                {
                    if (flight.PassengersCapacity > 0)
                    {
                        var reservation = AddReservation(Reservations, uniquePlaneNumber, flight);
                        flight.PassengersCapacity -= 1;
                        this.dbContext.Reservations.Add(reservation);
                    }
                    else
                    {
                        throw new ArgumentException(ExceptionMessages.NotEnoughAmountOfRegularTickets);
                    }

                }
                else if (Reservations.TicketType == GlobalConstants.TicketTypeBusinessClass)
                {
                    if (flight.BusinessClassCapacity > 0)
                    {
                        var reservation = AddReservation(Reservations, uniquePlaneNumber, flight);
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
        }
           
                /// <summary>
        /// Метод, който се извиква от горния. Създава се резервация. Отделен е, за да бъде по-четлив кода.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="secondName"></param>
        /// <param name="familyName"></param>
        /// <param name="pin"></param>
        /// <param name="email"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="nationality"></param>
        /// <param name="ticketType"></param>
        /// <param name="uniquePlaneNumber"></param>
        /// <param name="flight"></param>
        /// <returns>резервация за добавяне</returns>
        private Reservation AddReservation(ReservationViewModel reservationView, int uniquePlaneNumber, Flight flight)
        {
            var reservation = new Reservation();

            if (flight.UniquePlaneNumber == uniquePlaneNumber)
            {
                reservation.FirstName = reservationView.FirstName;
                reservation.SecondName = reservationView.SecondName;
                reservation.FamilyName = reservationView.FamilyName;
                reservation.PIN = reservationView.PIN;
                reservation.Email = reservationView.Email;
                reservation.TelephoneNumber = reservationView.TelephoneNumber;
                reservation.Nationality = reservationView.Nationality;
                reservation.FlightId = flight.Id;


                //Check ticket type
                var ticketTypeEntity = this.dbContext.TicketTypes.FirstOrDefault(x => x.Name == reservationView.TicketType);

                if (ticketTypeEntity == null)
                {
                    ticketTypeEntity = new TicketType()
                    {
                        Name = reservationView.TicketType
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
        
        /// <summary>
        /// Взима резервацията за даден полет по уникално число.
        /// </summary>
        /// <param name="uniquePlaneNumber"></param>
        /// <returns></returns>       

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
                    Email = r.Email,
                    TelephoneNumber = r.TelephoneNumber,
                    Nationality = r.Nationality,
                    TicketType = r.TicketType.Name
                })
                .ToList();

            return reservations;
        }

        public async Task<ReservationTableViewModel> ReturnPages(ReservationTableViewModel model)
        {
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<ReservationViewModel> items = dbContext.Reservations.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new ReservationViewModel()
            {                
               FirstName = c.FirstName,
               SecondName = c.SecondName,
               FamilyName = c.FamilyName,
               Email = c.Email,
               PIN = c.PIN,
               Nationality = c.Nationality,
               TelephoneNumber = c.TelephoneNumber,
               TicketType = c.TicketType.Name,
               FlightId = c.FlightId
               
            }).ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(dbContext.Reservations.Count() / (double)PageSize);

            return model;
        }
    }
}

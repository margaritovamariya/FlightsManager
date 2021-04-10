using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services.Tests.ReservationTests
{
    [TestFixture]
    public class ReservationServicesTests
    {

        [Test]
        public void Create_ShouldSuccessfulyAdd_To_Database()
        {
            //Arrange
            var data = flights.AsQueryable();

            var TicketsData = ticketTypes.AsQueryable();

            var mockSet = new Mock<DbSet<Reservation>>();

            var mockSetTickets = new Mock<DbSet<TicketType>>();
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.Provider).Returns(TicketsData.Provider);
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.Expression).Returns(TicketsData.Expression);
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.ElementType).Returns(TicketsData.ElementType);
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.GetEnumerator()).Returns(() => TicketsData.GetEnumerator());


            var mockSetFlight = new Mock<DbSet<Flight>>();
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSetFlight.Object);
            mockContext.Setup(x => x.TicketTypes).Returns(mockSetTickets.Object);
            mockContext.Setup(x => x.Reservations).Returns(mockSet.Object);
            var flight = data.FirstOrDefault(x => x.UniquePlaneNumber == 123456);

            reservationListView.Reservations = reservation;

            //Act
            var service = new ReservationService(mockContext.Object);
            service.Create(reservationListView, flight.UniquePlaneNumber);

            //Assert
            mockContext.Verify(x => x.SaveChanges(), Times.AtLeastOnce());
        }

        [Test]
        public void GetFlightReservations_Returns_Reservation_For_GivenFlight()
        {
            //Arrange
            var reservations = reservations1.AsQueryable();

            var data = flights.AsQueryable();

            var TicketsData = ticketTypes.AsQueryable();

            var mockSet = new Mock<DbSet<Reservation>>();
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservations.Provider);
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservations.Expression);
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservations.ElementType);
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => reservations.GetEnumerator());

            var mockSetTickets = new Mock<DbSet<TicketType>>();
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.Provider).Returns(TicketsData.Provider);
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.Expression).Returns(TicketsData.Expression);
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.ElementType).Returns(TicketsData.ElementType);
            mockSetTickets.As<IQueryable<TicketType>>().Setup(m => m.GetEnumerator()).Returns(() => TicketsData.GetEnumerator());


            var mockSetFlight = new Mock<DbSet<Flight>>();
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetFlight.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSetFlight.Object);
            mockContext.Setup(x => x.TicketTypes).Returns(mockSetTickets.Object);
            mockContext.Setup(x => x.Reservations).Returns(mockSet.Object);
            var flight = data.FirstOrDefault(x => x.UniquePlaneNumber == 123456);

            //Act
            var service = new ReservationService(mockContext.Object);
            var Result = service.GetFlightReservations(flight.UniquePlaneNumber);
            var expected = reservation.AsEnumerable();

            //Assert
            Assert.True(Object.Equals(expected.Count(), Result.Count())); 
        }

        [Test]
        public async Task ReturnPages_SuccessfulyReturnPageCount_And_ListOfUsers()
        {
            //Arrange
            var data = reservations1.AsQueryable();

            var mockSet = new Mock<DbSet<Reservation>>();
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Reservations).Returns(mockSet.Object);

            //Act
            var service = new ReservationService(mockContext.Object);
            var result = await service.ReturnPages(reservationTableView);

            //Assert
            Assert.That(result.Items.Count() == reservation.Count());
        }

        private readonly List<Reservation> reservations1 = new()
        {
            new Reservation
            {
                 FirstName = "Random",
                 SecondName = "Random",
                 FamilyName = "RandomAgain",
                 Email = "Random@Random.Random",
                 Nationality = "NqkuvSi",
                 PIN = 111111111,
                 TelephoneNumber = "+1111111",
                 TicketType = new TicketType()
                 {
                     Name = "Business Class"
                 },
                 FlightId = 1,
                 Id = 1,
                 Flight = new Flight()
                 {
                     UniquePlaneNumber = 123456
                 },
                 TicketTypeId = 1
            },

            new Reservation
            {
                 FirstName = "NqkuvSi",
                 SecondName = "NqkuvSi",
                 FamilyName = "NqkuvSiAgain",
                 Email = "Random@NqkuvSi.Random",
                 Nationality = "Random",
                 PIN = 222222222,
                 TelephoneNumber = "+2222222222",
                 TicketType = new TicketType()
                 {
                     Name = "Business Class"
                 },
                 FlightId = 2,
                 Id = 2,
                 Flight = new Flight()
                 {
                     UniquePlaneNumber = 123456
                 },
                 TicketTypeId = 2
            },

        };

        private readonly ReservationTableViewModel reservationTableView = new();

        private readonly List<ReservationViewModel> reservation = new()
        {
             new ReservationViewModel
             {
                 FirstName = "Random",
                 SecondName = "Random",
                 FamilyName = "RandomAgain",
                 Email = "Random@Random.Random",
                 Nationality = "NqkuvSi",
                 PIN = 111111111,
                 TelephoneNumber = "+1111111",
                 TicketType = "Business Class"
             },

             new ReservationViewModel
             {
                 FirstName = "NqkuvSi",
                 SecondName = "NqkuvSi",
                 FamilyName = "NqkuvSiAgain",
                 Email = "NqkuvSi@NqkuvSi.NqkuvSi",
                 Nationality = "Random",
                 PIN = 22222222,
                 TelephoneNumber = "+2222222",
                 TicketType = "Business Class"
             }

        };

        private readonly ReservationListViewModel reservationListView = new()
        {
            Reservations = new List<ReservationViewModel>()
        };

        private readonly List<TicketType> ticketTypes = new()
        {
            new TicketType
            {
                Id = 1,
                Name = "Regular",
                Reservations = new List<Reservation>()
            },

            new TicketType
            {
                Id = 2,
                Name = "Business Class",
                Reservations = new List<Reservation>()
            }
        };

        private readonly List<Flight> flights = new()
        {
             new Flight
             {
                Id = 1,
                From = "Varna",
                To = "Sofia",
                DateTimeLanding = DateTime.UtcNow,
                DateTimeTakeOff = DateTime.UtcNow.AddDays(2),
                PilotName = "Jeniffer",
                PlaneType = "Samoletonosach",
                UniquePlaneNumber = 123456,
                BusinessClassCapacity = 15,
                PassengersCapacity = 15,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        FirstName = "Random",
                        SecondName = "Random",
                        FamilyName = "RandomAgain",
                        Email = "Random@Random.Random",
                        Nationality = "NqkuvSi",
                        PIN = 111111111,
                        TelephoneNumber = "+1111111",
                        TicketType = new TicketType()
                        {
                            Name = "Business Class"
                        },
                        FlightId = 1,
                        Id = 1,
                        Flight = new Flight(),
                        TicketTypeId = 1
                    },

                    new Reservation
                    {
                        FirstName = "NqkuvSi",
                        SecondName = "NqkuvSi",
                        FamilyName = "NqkuvSiAgain",
                        Email = "NqkuvSi@NqkuvSi.NqkuvSi",
                        Nationality = "Random",
                        PIN = 22222222,
                        TelephoneNumber = "+2222222",
                        TicketType = new TicketType()
                        {
                            Name = "Business Class"
                        },
                        FlightId = 2,
                        Id = 2,
                        Flight = new Flight(),
                        TicketTypeId = 2
                    }
                }
             },

             new Flight
             {
                Id = 2,
                From = "Sofia",
                To = "Varna",
                DateTimeLanding = DateTime.UtcNow,
                DateTimeTakeOff = DateTime.UtcNow.AddDays(3),
                PilotName = "Samoletonosach",
                PlaneType = "Jeniffer",
                UniquePlaneNumber = 123456789,
                BusinessClassCapacity = 10,
                PassengersCapacity = 10,
                Reservations = new List<Reservation>()
             },

        };
    }
}

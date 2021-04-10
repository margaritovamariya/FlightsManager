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

namespace FlightManager.Services.Tests.NewFolder
{
    [TestFixture]
    public class FlightServicesTests
    {

        [Test]
        public void Create_AddsSuccessfulyToDatabase()
        {
            //Arrange
            var data = flightView;

            var Flights = flights.AsQueryable();

            var mockSet = new Mock<DbSet<Flight>>();
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(Flights.Provider);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(Flights.Expression);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(Flights.ElementType);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => Flights.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSet.Object);

            //Act
            var service = new FlightService(mockContext.Object);
            service.Create(data);

            //Assert
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void GetExactFlights_ReturnsFlightBy_GivenUniquePlaneNumber()
        {
            //Arrange
            var data = ExactOrAllflights.AsQueryable();

            var mockSet = new Mock<DbSet<Flight>>();
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSet.Object);
            var flight = data.FirstOrDefault(x => x.UniquePlaneNumber == 123456);

            //Act
            var service = new FlightService(mockContext.Object);
            var Result = service.GetExactFlight(flight.UniquePlaneNumber);

            //Assert
            Assert.AreEqual(1, Result.Count());
        }

        [Test]
        public void GetAllFlights_ReturnsAllFlightsFromDatabase()
        {
            var data = ExactOrAllflights.AsQueryable();

            var mockSet = new Mock<DbSet<Flight>>();
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSet.Object);

            //Act
            var service = new FlightService(mockContext.Object);
            var Result = service.GetAllFlights();

            //Assert
            Assert.AreEqual(2, Result.Count());
        }

        [Test]
        public void UpdateFlight_SuccessfulyChangedDatainDatabase()
        {
            //Arrange
            var data = ExactOrAllflights.AsQueryable();

            var mockSet = new Mock<DbSet<Flight>>();
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSet.Object);


            //Act
            var service = new FlightService(mockContext.Object);
            service.UpdateFlight(flightView);
            var result = mockContext.Object.Flights.FirstOrDefault(x => x.UniquePlaneNumber == flightView.UniquePlaneNumber);

            //Assert
            Assert.AreEqual(result.UniquePlaneNumber, flight1.UniquePlaneNumber);
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteFlight_ShouldRemoveDataFromDatabase_Successfuly()
        {
            //Arrange
            var data = ExactOrAllflights.AsQueryable();

            var ReservData = reservations1.AsQueryable();

            var mockSet = new Mock<DbSet<Flight>>();
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockSetReservations = new Mock<DbSet<Reservation>>();
            mockSetReservations.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(ReservData.Provider);
            mockSetReservations.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(ReservData.Expression);
            mockSetReservations.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(ReservData.ElementType);
            mockSetReservations.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => ReservData.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Flights).Returns(mockSet.Object);
            mockContext.Setup(x => x.Reservations).Returns(mockSetReservations.Object);
            var PlaneIdToDelete = data.FirstOrDefault(x => x.UniquePlaneNumber == ExactOrAllflights[0].UniquePlaneNumber).UniquePlaneNumber;

            //Act
            var service = new FlightService(mockContext.Object);
            service.DeleteFlight(PlaneIdToDelete);

            //Assert
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
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

        private readonly Flight flight1 = new()
        {
            From = "NqkadeSi",
            To = "DonqkadeSi",
            DateTimeTakeOff = DateTime.UtcNow,
            DateTimeLanding = DateTime.UtcNow.AddDays(2),
            PlaneType = "Doing",
            BusinessClassCapacity = 15,
            PassengersCapacity = 15,
            PilotName = "Petka",
            Reservations = new List<Reservation>(),
            UniquePlaneNumber = 123456
        };

        private readonly FlightViewModel flightView = new FlightViewModel()
        {
            From = "NqkadeSi",
            To = "DonqkadeSi",
            DateTimeTakeOff = DateTime.UtcNow,
            DateTimeLanding = DateTime.UtcNow.AddDays(2),
            PlaneType = "Doing",
            BusinessClassCapacity = 15,
            PassengerCapacity = 15,
            PilotName = "Petka",
            Duration = TimeSpan.Zero,
            Reservations = new List<ReservationViewModel>(),
            UniquePlaneNumber = 123456
        };

        private readonly List<Flight> flights = new List<Flight>();

        private readonly List<Flight> ExactOrAllflights = new List<Flight>()
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

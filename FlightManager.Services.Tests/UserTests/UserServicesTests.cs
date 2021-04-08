using FlightManager.Common;
using FlightManager.Models;
using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FlightsManager.Services.Tests
{
    [TestFixture]
    public class UserServicesTests
    {
        private readonly Mock<IUserServices> _userMockServices = new();

        [Test]
        public async Task FindAsync_ReturnsUserById_Correctly()
        {
            var expected = new User()
            {
                Id = "abc12-ccd21-2a2we-33sfE",
                UserName = "Neshto",
                NormalizedUserName = "Neshto",
                Email = "Email@emailmial.com",
                NormalizedEmail = "email@emailmial.com",
                FirstName = "Neshto",
                FamilyName = "Neshto",
                PIN = 22222222,
                Address = "Str. ExampleStreetMail",
                PhoneNumber = "+2222222222",
                EmailConfirmed = false,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            _userMockServices.Setup(x => x.FindAsync("abc12-ccd21-2a2we-33sfE")).ReturnsAsync(expected);

            var actual = _userMockServices.Object.FindAsync(expected.Id);

            Assert.AreEqual(expected, actual.Result);
        }

        //CHANGE NAME

        //[Test]
        //public void something()
        //{

        //}
    }
}
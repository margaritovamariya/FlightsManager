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

namespace FlightsManager.Services.Tests
{
    [TestFixture]
    public class UserServicesTests
    {
        private readonly Mock<IUserServices> _userMockServices = new();


        [Test]
        public void FindAsync_ReturnsUserById_Correctly()
        {
            //Arrange
            var expected = user;

            //Act
            _userMockServices.Setup(x => x.FindAsync("abc12-ccd21-2a2we-33sfE")).ReturnsAsync(expected);

            var actual = _userMockServices.Object.FindAsync(expected.Id);

            //Assert
            Assert.AreEqual(expected, actual.Result);
        }

        [Test]
        public void Create_CanBeSuccessfulyCalled()
        {
            _userMockServices.Setup(x => x.Create(It.IsAny<User>(), "password")).Verifiable();
            _userMockServices.Object.Create(It.IsAny<User>(), "password");
            Mock.Verify();
        }

        [Test]
        public void Create_ShouldAddToDatabase_Successfuly()
        {
            //Arrange
            var data = Users.AsQueryable();

            var dataRoles = roles.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockSetRole = new Mock<DbSet<IdentityRole>>();
            mockSetRole.As<IQueryable<IdentityRole>>().Setup(m => m.Provider).Returns(dataRoles.Provider);
            mockSetRole.As<IQueryable<IdentityRole>>().Setup(m => m.Expression).Returns(dataRoles.Expression);
            mockSetRole.As<IQueryable<IdentityRole>>().Setup(m => m.ElementType).Returns(dataRoles.ElementType);
            mockSetRole.As<IQueryable<IdentityRole>>().Setup(m => m.GetEnumerator()).Returns(() => dataRoles.GetEnumerator());

            var mockSetUserRole = new Mock<DbSet<IdentityUserRole<string>>>();

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);
            mockContext.Setup(x => x.Roles).Returns(mockSetRole.Object);
            mockContext.Setup(x => x.UserRoles).Returns(mockSetUserRole.Object);



            //Act
            var service = new UserServices(mockContext.Object);
            service.Create(UserToCreate, "password");

            //Assert
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void Update_SuccessfulyChangedDatainDatabase()
        {
            //Arrange
            var data = Users.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);


            //Act
            var service = new UserServices(mockContext.Object);
            service.Update(UserEditView);
            var result = mockContext.Object.Users.FirstOrDefault(x => x.UserName == UserEditView.UserName);

            //Assert
            Assert.AreEqual(result.UserName, UserEditView.UserName);
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void Delete_RemovesFromDataBase_Successfuly()
        {
            //Arrange
            var data = Users.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            var UserIdToDelete = data.FirstOrDefault(x => x.Id == user.Id).Id;

            //Act
            var service = new UserServices(mockContext.Object);
            service.Delete(UserIdToDelete);

            //Assert
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task ReturnPages_SuccessfulyReturnPageCount_And_ListOfUsers()
        {
            //Arrange
            var data = Users.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<FlightManagerDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            //Act
            var service = new UserServices(mockContext.Object);
            var result = await service.ReturnPages(userIndexViewModel);

            //Assert
            Assert.That(result.Items.Count() == userViewModels.Count());
        }


        //Variables for testing
        private readonly User UserToCreate = new User()
        {
            Id = "33sfE-abc12-ccd21-dsc2",
            UserName = "NqmaNishto",
            NormalizedUserName = "NqmaNishto",
            Email = "Email@NqmaNishto.com",
            NormalizedEmail = "email@NqmaNishto.com",
            FirstName = "NqmaNishto",
            FamilyName = "NqmaNishto",
            PIN = 33333332,
            Address = "Str. NqmaNishto",
            PhoneNumber = "+55555555",
            EmailConfirmed = false,
            LockoutEnabled = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        private readonly User user = new User()
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

        private readonly List<User> Users = new List<User>()
        {
            new User
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
            },

            new User
            {
               Id = "dsc2-ccd21-2a2we-33sfE",
               UserName = "Something",
               NormalizedUserName = "Something",
               Email = "Email@Something.com",
               NormalizedEmail = "email@Something.com",
               FirstName = "Something",
               FamilyName = "Something",
               PIN = 1111111111,
               Address = "Str. Something",
               PhoneNumber = "+3333333333",
               EmailConfirmed = false,
               LockoutEnabled = false,
               SecurityStamp = Guid.NewGuid().ToString()
            }
        };

        private readonly List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
               Id = "asd2-31a3-22ee-ra23w-ww23F",
               Name = "Worker",
               NormalizedName = "worker",
               ConcurrencyStamp = ""
            },

            new IdentityRole
            {
               Id = "dsa-31a3-22ee-ra23w-ww23F",
               Name = "Admin",
               NormalizedName = "admin",
               ConcurrencyStamp = ""
            }
        };

        private readonly List<UserViewModel> userViewModels = new List<UserViewModel>()
        {
            new UserViewModel
            {
               Id = "abc12-ccd21-2a2we-33sfE",
               UserName = "Neshto",
               Email = "email@emailmial.com",
               FirstName = "Neshto",
               FamilyName = "Neshto",
               Address = "Str. ExampleStreetMail",
               PhoneNumber = "+2222222222",
            },

            new UserViewModel
            {
               Id = "dsc2-ccd21-2a2we-33sfE",
               UserName = "Something",
               Email = "email@Something.com",
               FirstName = "Something",
               FamilyName = "Something",
               Address = "Str. Something",
               PhoneNumber = "+3333333333",
            }
        };

        private readonly UserIndexViewModel userIndexViewModel = new UserIndexViewModel();

        private readonly UserEditViewModel UserEditView = new UserEditViewModel()
        {
            Id = "abc12-ccd21-2a2we-33sfE",
            FirstName = "Random",
            FamilyName = "Random",
            Email = "Email@Random.bg",
            UserName = "UserNotName",
            PhoneNumber = "+2222333",
            Address = "Str. BackCityDotCom"
        };
    }
}
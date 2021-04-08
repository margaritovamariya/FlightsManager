using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FlightManager.Services.Models.OutputModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class UserServices : IUserServices
    {
        private const int PageSize = 10;
        private readonly FlightManagerDbContext dbContext;
        public UserServices(FlightManagerDbContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Seed-ване на базата данни при първоначално стартиране.
        /// </summary>
        //Seed Admin
        public void SeedRoles()
        {
            string[] roles = { "Admin", "Worker" };

            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                foreach (string role in roles)
                {
                    IdentityRole identityRole = new IdentityRole()
                    {
                        Name = role,
                        NormalizedName = role.ToLower()
                    };

                    if (!dbContext.Roles.Any(r => r.Name == role))
                    {
                        dbContext.Roles.Add(identityRole);
                    }
                }
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Seed-ване на базата данни при първоначално стартиране.
        /// </summary>
        //Seed Admin
        public void SeedUserRoles()
        {
            var getAdminId = dbContext.Roles.FirstOrDefault(x => x.Name == "Admin");
            var Roles = new IdentityUserRole<string>();
            var user = new User()
            {
                UserName = "Owner",
                NormalizedUserName = "Owner",
                Email = "Email@email.com",
                NormalizedEmail = "email@email.com",
                FirstName = "Owner",
                FamilyName = "Owner",
                PIN = 111111111,
                Address = "Str. ExampleStreet",
                PhoneNumber = "+1111111111",
                EmailConfirmed = false,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                Roles.RoleId = getAdminId.Id;
                Roles.UserId = user.Id;
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                dbContext.Users.Add(user);
                dbContext.UserRoles.Add(Roles);
            }

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Създаване на потребител асинхронно.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        //Create User
        public async void Create(User user, string Password)
        {
            var usernameExists = dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName);

            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, Password);
            user.PasswordHash = hashed;

            var CreateUser = new User()
            {
                UserName = user.UserName,
                NormalizedUserName = user.UserName,
                Email = user.Email,
                NormalizedEmail = user.Email,
                FirstName = user.FirstName,
                FamilyName = user.FamilyName,
                PIN = user.PIN,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.PasswordHash,
            };

            var userAdmin = dbContext.Roles.FirstOrDefault(x => x.Name == "Admin");
            var userWorker = dbContext.Roles.FirstOrDefault(x => x.Name == "Worker");
            var Roles = new IdentityUserRole<string>();


            if (this.dbContext.Users.Count() < 1)
            {
                Roles.RoleId = userAdmin.Id;
                Roles.UserId = CreateUser.Id;
            }
            else
            {
                Roles.RoleId = userWorker.Id;
                Roles.UserId = CreateUser.Id;

            }


            if (usernameExists == null)
            {
                await dbContext.Users.AddAsync(CreateUser);
                await dbContext.UserRoles.AddAsync(Roles);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.UsernameAlreadyExist);
            }

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Променяне на данните на дадения потребител.
        /// </summary>
        /// <param name="user"></param>
        public void Update(UserEditViewModel user)
        {
            var user1 = dbContext.Users.FirstOrDefault(x => x.Id == user.Id);

            user1.UserName = user.UserName;
            user1.NormalizedUserName = user.UserName;
            user1.Email = user.Email;
            user1.NormalizedEmail = user.Email;
            user1.FirstName = user.FirstName;
            user1.FamilyName = user.FamilyName;
            user1.Address = user.Address;
            user1.PhoneNumber = user.PhoneNumber;

            dbContext.Users.Update(user1);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Изтриване на дадения потребител.
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(string Id)
        {
            User user = dbContext.Users.Find(Id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Показване на лист от потребители на страницата.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> UserIndexViewModel модел </returns>
        //Render List of Users on the page
        public async Task<UserIndexViewModel> ReturnPages(UserIndexViewModel model)
        {
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<UserViewModel> items = await dbContext.Users.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new UserViewModel()
            {
                Id = c.Id,
                UserName = c.UserName,
                Email = c.NormalizedEmail,
                FirstName = c.FirstName,
                FamilyName = c.FamilyName,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,

            }).ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await dbContext.Users.CountAsync() / (double)PageSize);

            return model;
        }


        /// <summary>
        /// Намира потребител по дадено ид.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Връща намерения потребител </returns>
        //Find User By Id
        public async Task<User> FindAsync(string id)
        {
            User user = await dbContext.Users.FindAsync(id);

            return user;
        }

    }
}

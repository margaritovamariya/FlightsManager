﻿using FlightManager.Data;
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

        //Seed Admin
        public async void SeedUserRoles()
        {
            string[] roles = { "Admin", "Worker" };

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

            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(dbContext);

                    if (!dbContext.Roles.Any(r => r.Name == role))
                    {
                        await roleStore.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<User>(dbContext);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }

            await dbContext.SaveChangesAsync();
        }

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

        //public Task UpdateAsync(User user)
        //{
        //    var HiddenValues = dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
        //    var context = userStore.Context as FlightManagerDbContext;

        //    User user1 = new User
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        NormalizedUserName = user.UserName,
        //        Email = user.Email,
        //        NormalizedEmail = user.Email,
        //        FirstName = user.FirstName,
        //        FamilyName = user.FamilyName,
        //        PIN = HiddenValues.PIN,
        //        Address = user.Address,
        //        PhoneNumber = user.PhoneNumber,
        //        PasswordHash = HiddenValues.PasswordHash
        //    };

        //    HiddenValues = user1;

        //    context.Users.Attach(HiddenValues);

        //    context.Entry(HiddenValues).State = EntityState.Modified;

        //    return context.SaveChangesAsync();

        //}

        public void Update(User user)
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

            //User user1 = new User
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    NormalizedUserName = user.UserName,
            //    Email = user.Email,
            //    NormalizedEmail = user.Email,
            //    FirstName = user.FirstName,
            //    FamilyName = user.FamilyName,
            //    PIN = HiddenValues.Result.PIN,
            //    Address = user.Address,
            //    PhoneNumber = user.PhoneNumber,
            //    PasswordHash = HiddenValues.Result.PasswordHash
            //};

            dbContext.Users.Update(user1);
            dbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            User user = dbContext.Users.Find(Id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

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

        //Find User By Id
        public async Task<UserEditViewModel> FindAsync(string id)
        {
            User user = await dbContext.Users.FindAsync(id);

            UserEditViewModel model = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };

            return model;
        }

    }
}

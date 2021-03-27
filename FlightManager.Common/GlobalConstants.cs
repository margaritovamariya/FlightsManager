using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Common
{
    public class GlobalConstants
    {
        //Flight
        public const int FromMinLength = 3;
        public const int FromMaxLength = 30;
        public const int ToMinLength = 3;
        public const int ToMaxLength = 30;
        public const int UniquePlaneNumberMinLength = 3;    
        public const int PassengersCapacityMinLength = 5;    
        public const int PassengersCapacityMaxLength = 1000;    
        public const int BusinessClassMinCapacity = 5;
        public const int BusinessClassMaxCapacity = 200;

        //Reservation
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 30;
        public const int SecondNameMinLength = 3;
        public const int SecondNameMaxLength = 30;
        public const int FamilyNameMinLength = 3;
        public const int FamilyNameMaxLength = 30;
        public const int ClientPinMinLength = 5;

        //TicketType
        public const int TicketTypeMinLength = 3;
        public const string TicketTypeRegular = "Regular";
        public const string TicketTypeBusinessClass = "Business Class";

        //User
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 30;
        public const int PasswordMinLength = 3;
        public const int PasswordMaxLength = 30;
        public const int EmailMinLength = 3;
        public const int EmailMaxLength = 30;
        public const int UserPinMinLength = 5;
        public const int UserFirstNameMinLength = 2;
        public const int UserFirstNameMaxLength = 30;
        public const int UserFamilyNameMinLength = 3;
        public const int UserFamilyNameMaxLength = 30;

        //UserRole 
        public const string Admin = "Admin";
        public const string Worker = "Worker";

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Common
{
    public class ExceptionMessages
    {
        public const string InvalidPassengerAmount = "Invalid passenger amount provided!";
        public const string InvalidBusinessAmount = "Invalid business amount provided!";
        public const string InvalidUniqueNumberPlane = "Plane with this unique number doesn`t exist!";
        public const string InvalidFlight = "Flight whith these details doesn`t exist!";
        public const string InvalidReservation = "Reservation whith these id doesn`t exist!";
        public const string InvalidTicketType = "Type of this ticked doesn`t exist!";
        public const string NotEnoughAmountOfRegularTickets = "The regular tickets already finished. Try with business class ticket";
        public const string NotEnoughAmountOfBusinessClassTickets = "The Business Class tickets already finished. Try with regullar ticket";
        public const string PlaneWithThisUniqueNumberExist = "Plane with this unique number already exist! Use another unique number.";
        public const string UsernameAlreadyExist = "An account with this username already exist! please type another username.";
        public const string PasswordTooShort = "Please type a password with atleast 3 or more characters";
        public const string Telephone_Number_Already_Exists = "An account with this telephone number already exist! pleasy type another telephone number";
        public const string UsernameDoesntExist = "No account with the given username exists!";
    } 
}

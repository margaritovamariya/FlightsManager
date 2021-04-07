using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FlightManager.Common;

namespace FlightManager.Services.Models.OutputModels
{
    public class ReservationViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FamilyName { get; set; }
        public long PIN { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string Nationality { get; set; }
        public string TicketType { get; set; }
    }
}

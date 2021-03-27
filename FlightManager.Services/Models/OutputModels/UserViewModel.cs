using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }       
    }
}

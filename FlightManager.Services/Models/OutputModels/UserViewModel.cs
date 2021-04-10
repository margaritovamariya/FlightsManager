using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// UserModel
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// gets,sets id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// gets,sets Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// gets,sets Firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// gets,sets FamilyName
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// gets,sets Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// gets,sets Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// gets,sets phoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }       
    }
}

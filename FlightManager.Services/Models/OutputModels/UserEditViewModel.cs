using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// UserEditModel
    /// </summary>
    public class UserEditViewModel
    {
        /// <summary>
        /// Gets,sets Username
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// gets,sets Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets,sets Firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// gets,sets FamilyName
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// Gets,sets EMail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets,sets Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Gets,sets phonenumber
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}

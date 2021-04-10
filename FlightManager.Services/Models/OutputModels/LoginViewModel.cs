using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// LoginModel
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// gets,sets Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// gets,sets Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// gets,sets RememberMe
        /// </summary>
        public bool RememberMe { get; set; }
    }
}

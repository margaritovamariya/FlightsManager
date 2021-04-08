using FlightManager.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(GlobalConstants.UserFirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.UserFamilyNameMinLength)]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "The PIN field is required")]
        public long? PIN { get; set; }

        [Required]
        public string Address { get; set; }
    }
}

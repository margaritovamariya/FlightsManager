using FlightManager.Common;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(GlobalConstants.UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(GlobalConstants.PasswordMinLength)]
        public string Password { get; set; }

        [Required]
        [MinLength(GlobalConstants.EmailMinLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(GlobalConstants.UserFirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.UserFamilyNameMinLength)]
        public string FamilyName { get; set; }

        [Required]
        [MinLength(GlobalConstants.UserPinMinLength)]
        public long PIN { get; set; }

        public string Address { get; set; }

        public string TelephoneNumber { get; set; }

        //UserRole relation
        [Required]
        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}

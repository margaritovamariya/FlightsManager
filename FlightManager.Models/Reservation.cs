using FlightManager.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManager.Models
{
    public class Reservation
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.SecondNameMinLength)]
        public string SecondName { get; set; }

        [Required]
        [MinLength(GlobalConstants.FamilyNameMinLength)]
        public string FamilyName { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientPinMinLength)]
        public long PIN { get; set; }

        public string TelephoneNumber { get; set; }

        public string Nationality { get; set; }

        //TicketType relation
        [Required]
        public int TicketTypeId { get; set; }
        public virtual TicketType TicketType { get; set; }

        //Flight relation
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}

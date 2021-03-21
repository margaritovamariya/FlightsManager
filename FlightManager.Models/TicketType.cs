using FlightManager.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class TicketType
    {
        public TicketType()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.TicketTypeMinLength)]
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

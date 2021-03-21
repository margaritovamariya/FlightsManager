using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class UserRole
    {
        public UserRole()
        {
            this.Users = new HashSet<User>();
        }

        [Required]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

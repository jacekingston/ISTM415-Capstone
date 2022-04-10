using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrototype.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; }

        public int TeamId { get; set; } //Foreign Key
    }
}

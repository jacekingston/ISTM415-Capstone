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
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number, please ensure it is 10 digits.")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        //[RegularExpression(@"\w + ([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)", ErrorMessage = "Not a valid email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Zip code")]
        [Range(00000, 99999, ErrorMessage = "Invalid Length for Zip Code")]
        public int Zip { get; set; }

        public int TeamId { get; set; } //Foreign Key

        public Team Team { get; set; }
    }
}

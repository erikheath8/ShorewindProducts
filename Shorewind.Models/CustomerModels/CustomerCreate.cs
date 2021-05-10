using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.CustomerModels
{
    public class CustomerCreate
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(32, ErrorMessage = "Please enter less than 32 characters.")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(32, ErrorMessage = "Please enter less than 32 characters.")]
        public string LastName { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(64, ErrorMessage = "Please enter less than 64 characters.")]
        public string Address { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(64, ErrorMessage = "Please enter less than 64 characters.")]
        public string City { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(64, ErrorMessage = "Please enter less than 64 characters.")]
        public string State { get; set; }

        [StringLength(5, ErrorMessage = "Please enter 5 numbers for the ZIPCODE.")]
        public string PostalCode { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

    }
}

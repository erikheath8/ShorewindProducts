using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Data
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(32)]
        public string FirstName { get; set; }

        [Required, StringLength(64)]
        public string Address { get; set; }

        [Required, StringLength(64)]
        public string City { get; set; }

        [Required, StringLength(16)]
        public string State { get; set; }
        
        [Required, StringLength(5)]
        public string PostalCode { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

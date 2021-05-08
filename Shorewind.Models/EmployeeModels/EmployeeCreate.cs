using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.EmployeeModels
{
    public class EmployeeCreate
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(32, ErrorMessage = "Please enter less than 32 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(32, ErrorMessage = "Please enter less than 32 characters.")]
        public string LastName { get; set; }

        /* Uncomment after migration
         * 
         * [Required, EmailAddress]
            public string Email { get; set; }
         * 
        public DateTimeOffset CreatedUtc { get; set; }
        */
    }
}

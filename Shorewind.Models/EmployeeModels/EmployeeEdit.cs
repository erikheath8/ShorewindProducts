using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.EmployeeModels
{
    public class EmployeeEdit
    {
        [Key]
        public int EmployeeId { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(32, ErrorMessage = "Please enter less than 32 characters.")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(32, ErrorMessage = "Please enter less than 32 characters.")]
        public string LastName { get; set; }
          
        [EmailAddress]
        public string Email { get; set; }
          
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
          
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}

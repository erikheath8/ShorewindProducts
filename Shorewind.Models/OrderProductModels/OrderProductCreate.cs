using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.OrderProductModels
{
    public class OrderProductCreate
    {
        [Key]
        public int OrderProductId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int OrderId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter atleast 1 for the product count")]
        public int ProductCount { get; set; }
    }
}

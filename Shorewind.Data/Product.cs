using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int CategoryId { get; set; }

        [Required, Range(1, 255)]
        public int StockQuantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        /* Include for future added Column, migration on dbo
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
        */

    }
}

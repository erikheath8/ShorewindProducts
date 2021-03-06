using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Data
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }

        [Required, ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required, ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required, Range(1, Int32.MaxValue)]
        public int ProductCount { get; set; }

        // Add orderdate time with migration


    }
}

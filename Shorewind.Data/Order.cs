using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        /* Declared fields for shipping table
        [Required, ForeignKey(nameof(ShipAddress))]
        public int ShipAddressId { get; set; }

        public virtual ShipAddress ShipAddressActual { get; set; }
        */

        [Required, ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        [Required]
        public DateTimeOffset CreatedOrderDate { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        [NotMapped]
        public double TotalCost { 
            
            get {

                double cost = 0;
            
                foreach (OrderProduct orderProduct in OrderProducts)
                {
                    cost += (orderProduct.Product.UnitPrice * orderProduct.ProductCount);
                }

                return OrderProducts.Count > 0 ? cost : 0;

            } 
        }

        [DefaultValue(false)]
        public bool IsOrderShipped { get; set; }
    }
}

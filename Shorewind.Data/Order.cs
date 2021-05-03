using System;
using System.Collections.Generic;
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

        [Required, ForeignKey(nameof(CustomerId))]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public int EmployeeId { get; set; }

        public DateTimeOffset CreatedOrderDate { get; set; }

        public virtual ShipAddress ShipAddress { get; set; }
             
        public virtual List<OrderProduct> OrderProducts { get; set; }

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


    }
}

using Shorewind.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.OrderModels
{
    public class OrderList
    {
        public int OrderId { get; set; }

        /* Declared fields for shipping table
        public int ShipAddressId { get; set; }
        */

        public int CustomerId { get; set; }
               
        public int EmployeeId { get; set; }

        public virtual List<OrderProductList> OrderProducts { get; set; } = new List<OrderProductList>();

        /*
        public double TotalCost
        {

            get
            {

                double cost = 0;

                foreach (OrderProductList orderProduct in OrderProducts)
                {
                    cost += (orderProduct.UnitPrice * orderProduct.ProductCount);
                }

                return OrderProducts.Count > 0 ? cost : 0;

            }

            set {}
        }
        */

        public DateTimeOffset CreatedOrderDate { get; set; }

        public bool IsOrderShipped { get; set; }

    }
}

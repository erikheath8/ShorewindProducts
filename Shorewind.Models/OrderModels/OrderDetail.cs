using Shorewind.Data;
using Shorewind.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.OrderModels
{
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Employee Employee { get; set; }

        public List<OrderProductList> OrderProducts = new List<OrderProductList>();

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
        }

        public bool IsOrderShipped { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedOrderDate { get; set; }

    }
}

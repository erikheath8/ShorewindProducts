using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.OrderModels
{
    public class Orders
    {
        public int OrderId { get; set; }

        public DateTimeOffset CreatedOrderDate { get; set; }

        public bool IsOrderShipped { get; set; }

    }
}

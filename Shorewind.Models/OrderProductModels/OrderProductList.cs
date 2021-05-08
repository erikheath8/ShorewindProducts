using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.OrderProductModels
{
    public class OrderProductList
    {
        public int OrderProductId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductCount { get; set; }

        public double UnitPrice { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.ProductModels
{
    public class ProductDetail
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Quantity In Stock")]
        public int StockQuantity { get; set; }

        [Display(Name = "Price")]
        public double UnitPrice { get; set; }

    }
}

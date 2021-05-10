using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Models.ProductModels
{
    public class ProductEdit
    {
        [Display(Name = "Product Id")]
        [Range(0, int.MaxValue, ErrorMessage = "The Product Id must be greater than 0.")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Product Name should be a min of 2 Characters.")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Product Description should be a min of 4 Characters.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Quantity in Stock")]
        [Range(0, int.MaxValue, ErrorMessage = "The Product Quantity entered must be greater than 0.")]
        public int StockQuantity { get; set; }

        [Display(Name = "Product Price")]
        [Range(0, int.MaxValue, ErrorMessage = "The Unit Price must be greater than 0.")]
        public double UnitPrice { get; set; }

        /*  Uncomment after migration
         *   [Display(Name = "Modified")]
             public DateTimeOffset? ModifiedUtc { get; set; }
        */
    }
}

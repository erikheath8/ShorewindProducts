using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Data
{
    public class ShipAddress
    {
        //public int ShipAddressId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey(nameof(ShipOrder))]
        public int OrderId { get; set; }

        public virtual Order ShipOrder { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required, ForeignKey(nameof(ShipCustomer))]
        public Guid CustomerId { get; set; }

        public virtual Customer ShipCustomer { get; set; }

        [StringLength(16)]
        public string ShipFirstName { get; set; }

        [StringLength(16)]
        public string ShipLastName { get; set; }

        [StringLength(32)]
        public string ShipStreet { get; set; }

        [StringLength(32)]
        public string ShipCity { get; set; }

        [StringLength(16)]
        public string ShipState { get; set; }

        [StringLength(5)]
        public string ShipPostalCode { get; set; }

    }
}

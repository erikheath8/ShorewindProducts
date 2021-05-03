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
        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }

        [Required, ForeignKey(nameof(CustomerId))]
        public Guid CustomerId { get; set; }

        public string ShipFirstName { get; set; }

        public string ShipLastName { get; set; }

        public string ShipStreet { get; set; }

        public string ShipCity { get; set; }

        public string ShipState { get; set; }

        [StringLength(5)]
        public string ShipPostalCode { get; set; }

    }
}

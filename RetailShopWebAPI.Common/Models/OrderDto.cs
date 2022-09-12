using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class OrderDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public decimal Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class CreateOrderDto
    {
        [Required]
        public DateTime OrderDate { get; set; }
        public decimal Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int AddressId { get; set; }
        public List<CreateOrderOrderDetailDto> OrderDetails { get; set; }

    }
}

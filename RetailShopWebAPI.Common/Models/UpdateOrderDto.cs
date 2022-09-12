using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class UpdateOrderDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public decimal Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int AddressId { get; set; }
        public List<UpdateOrderDetailDto>? OrderDetails { get; set; }
    }
}

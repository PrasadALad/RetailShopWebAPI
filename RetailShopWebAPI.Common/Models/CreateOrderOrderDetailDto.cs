using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class CreateOrderOrderDetailDto
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public int ProductId { get; set; }

    }
}

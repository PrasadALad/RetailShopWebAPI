using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Entities
{
    public class OrderDetail
    {
        [Key]
        [Column("ORDER_DETAIL_ID")]
        public int OrderDetailId { get; set; }
        [Column("PRICE")]
        public decimal Price { get; set; }
        [Column("QUANTITY")]
        public int Quantity { get; set; }
        [Column("DISCOUNT")]
        public decimal Discount { get; set; }
        [Column("TOTAL_AMOUNT")]
        public decimal TotalAmount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

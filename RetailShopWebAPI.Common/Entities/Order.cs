using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Entities
{
    public class Order
    {
        [Key]
        [Column("ORDER_ID")]
        public int OrderId { get; set; }
        [Column("ORDER_DATE")]
        public DateTime OrderDate { get; set; }
        [Column("PAID_AMOUNT")]
        public decimal Paid { get; set; }
        [Column("PAYMENT_DATE")]
        public DateTime? PaymentDate { get; set; }
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}

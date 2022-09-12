using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Entities
{
    public class Product
    {
        [Key]
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }
        [Column("NAME")]
        public string ProductName { get; set; }
        [Column("DESCRIPTION")]
        public string ProductDescription { get; set; }
        [Column("MSRP")]
        public decimal MSRP { get; set; }
        [Column("UNITS_IN_STOCK")]
        public int UnitsInStock { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


    }
}

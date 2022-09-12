using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Entities
{
    public class Address
    {
        [Key]
        [Column("ADDRESS_ID")]
        public int AddressId { get; set; }
        [Column("NAME")]
        public string AddressName { get; set; }
        [Column("ADDR_LINE_1")]
        public string AddressLine1 { get; set; }
        [Column("ADDR_LINE_2")]
        public string AddressLine2 { get; set; }
        [Column("PINCODE")]
        public long Pincode { get; set; }
        public Customer Customer { get; set; }
        public List<Order> Orders {get; set; }
    }
}

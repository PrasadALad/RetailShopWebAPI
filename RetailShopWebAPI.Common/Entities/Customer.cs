using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Entities
{
    public class Customer
    {
        [Key]
        [Column("CUSTOMER_ID")]
        public int CustomerId { get; set; }
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }
        [Column("LAST_NAME")]
        public string LastName { get; set; }
        [Column("EMAIL_ID")]
        public string EmailId { get; set; }
        [Column("CONTACT_NO")]
        public string ContactNo { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Order> Orders { get; set; }

    }
}

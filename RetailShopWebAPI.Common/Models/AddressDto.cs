using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public long Pincode { get; set; }
    }
}

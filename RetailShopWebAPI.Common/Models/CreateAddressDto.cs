using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class CreateAddressDto
    {
        [Required(AllowEmptyStrings = false)]
        public string AddressName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public long Pincode { get; set; }
        [Required]
        public int customerId { get; set; }
    }
}

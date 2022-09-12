using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace RetailShopWebAPI.Common.Models
{
    public class CustomerDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}

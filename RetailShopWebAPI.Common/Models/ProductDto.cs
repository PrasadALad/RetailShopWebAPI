using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Models
{
    public class ProductDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        public decimal MSRP { get; set; }
        [DefaultValue(0)]
        public int UnitsInStock { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [Display (Name ="Brand")]
        public string BrandName { get; set; }
        [Display(Name = "Description")]
        public string BrandDescription { get; set; }
        public List<Product> Products { get; set; }
    }
}

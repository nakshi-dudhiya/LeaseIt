using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.ViewModels
{
    public class NewProductVM
    {
        [Required (ErrorMessage = "Product Name is Required.")]
        [Display (Name ="Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is Required.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Total Price is Required.")]
        [Display(Name = "Total Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Lease Price is Required.")]
        [Display(Name = "Lease Price")]
        public decimal LeasePrice { get; set; }

        [Required(ErrorMessage = "Image URL is Required.")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "ThumbnailImage is Required.")]
        [Display(Name = "Image Thumbnail URL")]
        public string ImageThumbnail { get; set; }

        [Required(ErrorMessage = "Color is Required.")]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Sale is Required.")]
        public bool IsOnSale { get; set; }

        [Required(ErrorMessage = "InStock is Required.")]
        public bool IsInStock { get; set; }

        [Required(ErrorMessage = "Brand is Required.")]
        [Display(Name = "Brand" ,Description ="Select a Brand")]
        public int BrandId { get; set; }
        
    }
}

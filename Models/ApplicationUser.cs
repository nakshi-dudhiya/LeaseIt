using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display (Name="Street Address")]
        public string StreetAdress { get; set; }
        
        [Required , Display(Name = "City")]
        public string City { get; set; }

        [Required, Display(Name = "Province")]
        public string Province { get; set; }

        [Required, Display(Name = "Country")]
        public string Country { get; set; }

        [Required, Display(Name = "Postal Code")]
        public string ZipCode { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public string ShoppingCartId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
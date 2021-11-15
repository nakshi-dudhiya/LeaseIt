using LeaseIt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaseIt.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        //Onmodel creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 1,
                BrandName = "Apple",
                BrandDescription = " It uses IOS Operating System."
            });
            builder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 2,
                BrandName = "Samsung",
                BrandDescription = " It uses Android Operating System."
            });
            builder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 3,
                BrandName = "Google",
                BrandDescription = " It uses Android Operating System."
            });
            builder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 4,
                BrandName = "OnePlus",
                BrandDescription = " It uses Android Operating System."
            });


            //adding products
            builder.Entity<Product>().HasData(new Product 
            {
                ProductId = 1,
                ProductName = "Iphone 13",
                Description = "Cinematic mode adds shallow depth of field automatically. " +
                "You can also shift focus after you shoot.Ceramic Shield is tougher than any smartphone glass. " +
                "And you get dust, spill and water resistance.",
                Price = 1099.00m,
                LeasePrice = 34.34m,
                ImageUrl = "\\Images\\iphone13.jpg",
                ImageThumbnail = "\\Images\\thumbnail\\iphone13.jpg",
                Color = "Pink, Blue, Midnight, Starlight, Red",
                IsInStock = true,
                IsOnSale = false,
                BrandId =1
            });

            builder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                ProductName = "Samsung Z Fold3 5G",
                Description = "The first Galaxy foldable that is compatible with S Pen.1 " +
                "The sleek construction and ultra-low latency2 creates a real pen-to-paper-feel for the most comfortable writing experience on a Galaxy yet." +
                "Its IPX8 Water Resistance. " +
                "The strongest aluminum frame on a Galaxy Foldable yet for added peace-of-mind.",
                Price = 2269.99m,
                LeasePrice = 70.94m,
                ImageUrl = "\\Images\\Zfold3.jpg",
                ImageThumbnail = "\\Images\\thumbnail\\Zfold3.jpg",
                Color = "Phantom Black, Phantom Green, Phantom Silver",
                IsInStock = true,
                IsOnSale = false,
                BrandId= 2
            });

            builder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                ProductName = "One Plus 9 Pro",
                Description = " It includes the features like Hyper Touch, Reading Mode, Night Mode, Vibrant Color Effect Pro" +
                ", Motion Graphics Smoothing, Ultra-high Video Resolution,Adaptive Display.",
                Price = 1069.00m,
                LeasePrice = 33.41m,
                ImageUrl = "\\Images\\Oneplus9pro.jpg",
                ImageThumbnail = "\\Images\\thumbnail\\Oneplus9pro.jpg",
                Color = "Pine Green, Morning Mist",
                IsInStock = true,
                IsOnSale = true,
                BrandId = 4
            });
        }
    }
}

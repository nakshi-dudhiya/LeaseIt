﻿using LeaseIt.Data;
using LeaseIt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart (ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.ProductId == product.ProductId &&
                n.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            } else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.ProductId == product.ProductId &&
                n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId ==
                ShoppingCartId).Include(x => x.Product).ToList());
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(x =>
                x.Product.LeasePrice * x.Amount).Sum();

            return total;
        }

        public void ClearShoppingCart()
        {
            var items = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}

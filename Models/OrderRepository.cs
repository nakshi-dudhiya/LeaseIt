using LeaseIt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Order> GetOrdersByUserIdAndRoles(string userId, string userRole)
        {
            var orders =   _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Product).Include(n =>
                n.User).ToList();

            if( userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

             _context.Orders.Add(order);
             _context.SaveChanges();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.ProductId,
                    OrderId = order.Id,
                    Price = (double)item.Product.LeasePrice
                };

                 _context.OrderItems.Add(orderItem);
            }

             _context.SaveChanges();

        }
    }
}

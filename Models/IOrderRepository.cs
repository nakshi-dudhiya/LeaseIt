using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public interface IOrderRepository
    {
        void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        List<Order> GetOrdersByUserIdAndRoles(string userId, string userRole);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Interfaces
{
   public interface IStoreRepository : IDisposable
    {
        IEnumerable<Customer> GetNames(string search = null);
        IEnumerable<Order> GetOrders(Customer customer = null);
        IEnumerable<Inventories> GetInventories(Customer customer = null);
        IEnumerable<ComponentInventory> GetComponents(Product product = null);

        IEnumerable<ProductCat> GetCategory(Product product = null);

        IEnumerable<Location> GetLocationAtId(int ID = -1);
        IEnumerable<OrderDetails> GetOrderDetailsAtId(int ID = -1);
        IEnumerable<Product> GetRecommended(ProductCat productCat = null);

        IEnumerable<OrderDetails> GetOrderDetails(Order order = null);
        IEnumerable<Customer> GetCustomerAtID(int ID = -1);
        IEnumerable<Order> GetOrdersL(Location customer = null);
        IEnumerable<Order> GetOrdersList(List<Customer> customer = null);
        IEnumerable<Order> GetOrdersAtId(int ID = -1);
        int GetProductAmount(Product product, Order order);

        IEnumerable<Product> DisplayProducts(Order order = null);
        void AddOrder(Order order, Location location, Customer customer);
        int RecentOrderID();
        int getInvId(Product p, Order order);
        void AddOrderDetails(OrderDetails orderDetails, Order order, Product product);
        void UpdateOrderDetails(OrderDetails orderDetails);
        void UpdateInventory(Inventories inventories);
        void Save();

    }
}

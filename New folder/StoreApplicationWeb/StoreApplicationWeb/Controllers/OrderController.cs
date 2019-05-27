using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApplication.DataAccess;
using StoreApplication.DataAccess.Entities;
using StoreApplication.Library;
using StoreApplication.Library.Interfaces;
using StoreApplicationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace StoreApplicationWeb.Controllers
{

    public class OrderController : Controller
    {
        public StoreApplicationContext dbContext = StoreRepository.CreateDbContext();
        public IStoreRepository StoreRepo { get; set; } 
        // GET: Order
        public ActionResult Index(string StoreId = "", string CustomerId = "", string Sort = "", string search = "")
        {
            
            StoreRepo = new StoreRepository(dbContext);
            IEnumerable<Order> items;
            if(search == "")
            {
                items = StoreRepo.GetOrders();
            }
            else
            {
                var customerSearch = StoreRepo.GetNames(search).ToList();
                items = StoreRepo.GetOrdersList(customerSearch);
            }
            if(CustomerId != "")
            {
                int parsed = 0;
                int.TryParse(CustomerId, out parsed);
                items = StoreRepo.GetOrders(StoreRepo.GetCustomerAtID(parsed).ToList().FirstOrDefault());
            }
            if (StoreId != "")
            {
                int parsed = 0;
                int.TryParse(StoreId, out parsed);
                items = StoreRepo.GetOrdersL(StoreRepo.GetLocationAtId(parsed).ToList().FirstOrDefault());
            }
            if(Sort == "Early")
            {
                items = items.OrderBy(x => x.OrderId);
            }
            else if (Sort == "Late")
            {
                items = items.OrderByDescending(x => x.OrderId);
            }
            else if (Sort == "Cheap")
            {
                items = items.OrderBy(x => x.TotalAmount);
            }
            else if (Sort == "Expensive")
            {
                items = items.OrderByDescending(x => x.TotalAmount);
            }
            var viewModels = items.Select(c => new ModelOrder
            {
                Customer = StoreRepo.GetCustomerAtID(c.CustomerId).ToList().FirstOrDefault(),
                CustomerId =c.CustomerId,
                Location = c.Location,
                TotalAmount = c.TotalAmount,
                OrderDetails = c.OrderDetails,
                OrderId = c.OrderId,
                StoreId = c.StoreId,
                TimeStamp = c.TimeStamp,

            }).ToList();
            // var viewModels = customers.ToList();
            ViewBag.numOfOrders = items.Count();
            StoreRepo.Dispose();
            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Details([Microsoft.AspNetCore.Mvc.FromQuery] int OrderId)
        {
            StoreRepo = new StoreRepository(dbContext);
            int id2 = OrderId;
            var items = StoreRepo.GetOrdersAtId(OrderId);
            var products = StoreRepo.DisplayProducts(items.ElementAt(0)).ToList();
            foreach(var p in products)
            {
                p.quantitySale = StoreRepo.GetProductAmount(p,items.ElementAt(0));
            }
            var viewModels = items.Select(c => new ModelOrder
            {
                Customer = c.Customer,
                CustomerId = c.CustomerId,
                Location = c.Location,
                TotalAmount = c.TotalAmount,
                OrderDetails = c.OrderDetails,
                OrderId = c.OrderId,
                StoreId = c.StoreId,
                TimeStamp = c.TimeStamp,
                Products = products,

            }).ToList().FirstOrDefault();

            ViewBag.numOfOrders = items.Count();
            return View(viewModels);

        }

        // GET: Order/Create
        public ActionResult Create(string error = "")
        {
            StoreRepo = new StoreRepository(dbContext);
            var test = error;
            var products = StoreRepo.DisplayProducts().OrderBy(x => x.ProductId).ToList();
            var Customers = StoreRepo.GetNames().ToList();
            List<SelectListItem> chooseCust = new List<SelectListItem>();
            var list = new List<SelectListItem>();

            for(int i = 0; i < 100; i++)
            {
                list.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
          
            foreach (var customer in Customers)
            {
                chooseCust.Add(new SelectListItem() { Text = customer.GetFullName(), Value = customer.CustomerId.ToString() });

            }

            ViewBag.list = list;

            var viewModel = new ModelOrder
            {
                LocationList = Mapper.Map(dbContext.Store).ToList(),
                chooseCust = chooseCust,
                Products = products,
                chooseProd = list,
                error = error,
            };
            
            
            return View(viewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelOrder order)
        {

            try
            {
                StoreRepo = new StoreRepository(dbContext);
                decimal total = 0;
                var products = StoreRepo.DisplayProducts().OrderBy(q => q.ProductId).ToList();
                var inventories = StoreRepo.GetInventories();
                inventories = inventories.Where(z => z.StoreId == order.StoreId);
                var pAmount = StoreRepo.DisplayProducts().OrderBy(q => q.ProductId).ToList();
                int counter = 0;
                foreach (var p in pAmount)
                { 
                    p.quantitySale = order.Products[counter].quantitySale;
                    counter++;   
                }

                counter = 0;
                foreach(var p in pAmount)
                {
                    if (p.quantitySale > 0)
                    {
                        counter++;
                        break;
                    }
                }

                if(counter <= 0)
                {
                    order.error = "No itemes were added";
                    string error = order.error;
                    return RedirectToAction("Create", "Order", new { error });
                }

                foreach(var p in pAmount)
                {

                    if (p.HasComponents && p.quantitySale > 0)
                    {
                        var componentInventories = StoreRepo.GetComponents(p);
                        var ComponentCart = new List<Product>();
                        foreach (var b in componentInventories)
                        {
                            foreach (var n in products)
                            {
                                if (b.ComponentProductId == n.ProductId)
                                {
                                    ComponentCart.Add(n);
                                }
                            }
                        }
                        foreach(var c in ComponentCart)
                        {
                            foreach(var v in pAmount)
                            {
                                if(c.ProductId == v.ProductId)
                                {
                                    v.quantitySale += p.quantitySale;
                                    total -= v.ProductCost*p.quantitySale;
                                }
                            }
                        }
                        total += p.ProductCost* p.quantitySale;
                        p.quantitySale = 0;
                    }
                }
                var testComponents = pAmount;
                counter = 0;
                foreach(var p in products)
                {
                    var inventoryP = inventories.Where(a => a.ProductId == p.ProductId).Select(o => o.Quantity).ToList().FirstOrDefault();

                    if (inventoryP - pAmount[counter].quantitySale < 0)
                    {
                        order.error = "Product: " + p.ProductName + " only has " + inventoryP + " left in Inventory";
                        string error = order.error;
                        return RedirectToAction("Create", "Order", new { error });
                    }
                    else
                    {
                        pAmount[counter].Quantity = inventoryP - pAmount[counter].quantitySale;
                    }
                    counter++;
                }

                foreach(var p in pAmount)
                {
                    total += p.quantitySale * p.ProductCost;
                }
                // TODO: Add insert logic here
                var newOrder = new Order()
                {
                    CustomerId = order.CustomerId,
                    StoreId = order.StoreId,
                    TotalAmount = total,
                    Location = StoreRepo.GetLocationAtId(order.StoreId).ToList().FirstOrDefault(),
                    Customer = StoreRepo.GetCustomerAtID(order.CustomerId).ToList().FirstOrDefault(),
                    OrderDetails = StoreRepo.GetOrderDetailsAtId(order.OrderId).ToList(),
                    OrderId = order.OrderId,
                    Products = order.Products,
                    TimeStamp = order.TimeStamp,
                };
                var x = StoreRepo.GetLocationAtId(newOrder.StoreId).ToList().FirstOrDefault();
                var y = StoreRepo.GetCustomerAtID(newOrder.OrderId).ToList().FirstOrDefault();
                order.Customer = y;
                order.Location = x;
                var custOrd = StoreRepo.GetOrders(y).ToList();
                foreach(var ord in custOrd)
                {
                    int dt = (int)(DateTime.UtcNow - ord.TimeStamp).TotalMinutes;
                    if ( dt < 120d && ord.StoreId == order.StoreId && ord.CustomerId == order.CustomerId)
                    {
                        order.error = "Please wait:" + (120 - dt) + " minutes to place an order at " + x.Name;
                        string error = order.error;
                        return RedirectToAction("Create", "Order", new { error });
                    }
                }
                StoreRepo.AddOrder(newOrder, newOrder.Location, newOrder.Customer);
                StoreRepo.Save();
                Thread.Sleep(20);
                var tempOrderId = dbContext.Orders.OrderByDescending(n => n.OrderId).Select(a => a.OrderId).FirstOrDefault();
                foreach(var p in pAmount)
                {
                    OrderDetails newOrderDetails = new OrderDetails
                    {
                        OrderId = tempOrderId,
                        ProductId = p.ProductId,
                        Quantity = p.quantitySale,
                    };
                    if (p.quantitySale > 0)
                    {
                        StoreRepo.AddOrderDetails(newOrderDetails, newOrder, p);
                        StoreRepo.Save();
                        Thread.Sleep(20);
                        var inventoryId = dbContext.Inventory.Where(n => n.ProductId == p.ProductId && n.StoreId == order.StoreId).Select(a => a.InventoryId).First();
                        Inventories inventory = new Inventories
                        {
                            Quantity = p.Quantity,
                            StoreId = order.StoreId,
                            ProductId = p.ProductId,
                            InventoryId = inventoryId,
                        };
                        StoreRepo.UpdateInventory(inventory);
                        StoreRepo.Save();
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
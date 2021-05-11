using Shorewind.Data;
using Shorewind.Models.OrderModels;
using Shorewind.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Services
{
    public class OrderService
    {
        private readonly Guid _userId;
        public OrderService(Guid userId)
        {
            _userId = userId;
        }

        public string CreateOrder(int customerId)
        {
            var entity =
                new Order()
                {
                    CustomerId = customerId,
                    CreatedOrderDate = DateTime.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);

                ctx.SaveChanges();

                return $"The Order ID: {entity.OrderId} has been created."; 
            }
        }

        public IEnumerable<Orders> GetOrders() 
        {

            using (var ctx = new ApplicationDbContext())
            {
                var orders =
                    ctx
                    .Orders.ToList();

                List<Orders> newList = new List<Orders>();

                foreach (var q in orders)
                {
                    var ordersList = new Orders
                    {
                        OrderId = q.OrderId,
                        CreatedOrderDate = q.CreatedOrderDate,
                        IsOrderShipped = q.IsOrderShipped
                    };
                    newList.Add(ordersList);
                }
                return newList;
            }
        }

        public OrderList GetMostRecentOrder(int customerId) 
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == customerId)
                    .OrderByDescending(e => e.CreatedOrderDate)
                    .First();

                return
                    new OrderList
                    {
                        OrderId = entity.OrderId,
                        CustomerId = entity.CustomerId,
                        OrderProducts = ListItemConverter(entity.OrderProducts),
                        CreatedOrderDate = entity.CreatedOrderDate,
                        //TotalCost = entity.TotalCost,
                        IsOrderShipped = entity.IsOrderShipped
                    };
            }

        }

        public OrderDetail GetOrderById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                ctx
                .Orders
                .Single(e => e.OrderId == id);

                return new OrderDetail
                    {
                        OrderId = entity.OrderId,
                        CustomerId = entity.CustomerId,
                        Customer = entity.Customer,
                        Employee = entity.Employee,
                        OrderProducts = entity.OrderProducts.Select(e => new OrderProductList()
                        {
                            OrderProductId = e.Product.ProductId,
                            ProductId = e.Product.ProductId,
                            ProductName = e.Product.ProductName,
                            ProductCount = e.ProductCount,
                            UnitPrice = e.Product.UnitPrice

                        }).ToList(),
                        //TotalCost = entity.TotalCost,
                        CreatedOrderDate = entity.CreatedOrderDate,
                        IsOrderShipped = entity.IsOrderShipped
                };
            }
        }

        public bool UpdateOrderToShipped(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Orders
                    .Single(e => e.OrderId == id);

                entity.IsOrderShipped = true;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOrder(int orderID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var order =
                    ctx
                    .Orders
                    .Single(e => e.OrderId == orderID);


                if (order.OrderProducts.Count > 0)
                {
                    foreach (OrderProduct orderProduct in order.OrderProducts.ToList())
                    {
                        var product =
                            ctx
                            .Products
                            .Single(p => p.ProductId == orderProduct.ProductId);

                        //return orderProduct to product inventory
                        product.StockQuantity += orderProduct.ProductCount;
                        ctx.OrderProducts.Remove(orderProduct);
                    }
                }

                ctx.Orders.Remove(order);

                return ctx.SaveChanges() >= 1;
            }
        }
       
        // Helper casting method
        public List<OrderProductList> ListItemConverter(List<OrderProduct> orderProducts)
        {
            List<OrderProductList> newList = new List<OrderProductList>();
            foreach (var op in orderProducts)
            {
                var listItem = new OrderProductList
                {
                    OrderProductId = op.OrderProductId,
                    ProductId = op.ProductId,
                    ProductName = op.Product.ProductName,
                    ProductCount = op.ProductCount,
                    UnitPrice = op.Product.UnitPrice,
                };
                newList.Add(listItem);
            }
            return newList;
        }

    }
}

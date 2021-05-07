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

        public string CreateOrder()//no input needed, all info is given already to create the foundation of an order
        {
            var entity =
                new Order()
                {
                    CustomerId = _userId,
                    CreatedOrderDate = DateTime.Now,
                    IsOrderShipped = false,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);

                ctx.SaveChanges();

                return $"The Order ID: {entity.OrderId} has been created."; //when bool, ctx.savechanges == 1
            }
        }

        public IEnumerable<OrderList> GetOrders() //For customer that is logged in
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == _userId).ToList();

                List<OrderList> newList = new List<OrderList>();

                foreach (var q in query)
                {
                    var oLI = new OrderList
                    {
                        OrderId = q.OrderId,
                        CustomerId = q.CustomerId,
                        OrderProducts = ListItemConverter(q.OrderProducts),
                        CreatedOrderDate = q.CreatedOrderDate,
                        IsOrderShipped = q.IsOrderShipped
                    };
                    newList.Add(oLI);
                }

                return newList;
            }
        }

        public OrderList GetMostRecentOrder() //For customer that is logged in
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == _userId)
                    .OrderByDescending(e => e.CreatedOrderDate)
                    .First();

                return
                    new OrderList
                    {
                        OrderId = entity.OrderId,
                        CustomerId = entity.CustomerId,
                        OrderProducts = ListItemConverter(entity.OrderProducts),
                        CreatedOrderDate = entity.CreatedOrderDate,
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

                return
                    new OrderDetail
                    {
                        OrderId = entity.OrderId,
                        CustomerId = entity.CustomerId,
                        CustomerFirstName = entity.Customer.FirstName,
                        OrderProducts = ListItemConverter(entity.OrderProducts),
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

       
        public List<OrderProductList> ListItemConverter(List<OrderProduct> orderProducts)
        {
            List<OrderProductList> newList = new List<OrderProductList>();
            foreach (var op in orderProducts)
            {
                var listItem = new OrderProductList
                {
                    OrderId = op.OrderId,
                    OrderProductId = op.OrderProductId,
                    ProductId = op.ProductId,
                    ProductCount = op.ProductCount,
                    ProductName = op.Product.ProductName,
                    UnitPrice = op.Product.UnitPrice,
                };
                newList.Add(listItem);
            }
            return newList;
        }


    }
}

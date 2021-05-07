using Shorewind.Data;
using Shorewind.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Services
{
    public class OrderProductService
    {
        private readonly Guid _userId;

        public OrderProductService(Guid userId)
        {
            _userId = userId;
        }

        public string CreateOrderProduct(OrderProductCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var order =
                    ctx
                    .Orders
                    .Single(o => o.OrderId == model.OrderId);

                if (order.IsOrderShipped)
                {
                    return "This order reached our Shipping Department and Changes can not be at this time.";
                }

                var entity =
                new OrderProduct()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId,
                    ProductCount = model.ProductCount
                };

                var product =
                    ctx
                    .Products
                    .Single(p => p.ProductId == model.ProductId);

                if (model.ProductCount > product.StockQuantity)
                    return $"Insufficient quantity of {product.ProductName} available." +
                        $"Current inventory quantity {product.StockQuantity}.";

                product.StockQuantity -= model.ProductCount;

                ctx.OrderProducts.Add(entity);
                ctx.SaveChanges();

                //when bool, savechanges == 2
                return $"The ProductId: {entity.ProductId} has been ordered."; 
            }
        }

        public IEnumerable<OrderProductList> GetOrderProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .OrderProducts
                    .Select(
                        e =>
                        new OrderProductList
                        {
                            OrderProductId = e.OrderProductId,
                            OrderId = e.OrderId,
                            ProductId = e.ProductId,
                            ProductName = e.Product.ProductName,
                            ProductCount = e.ProductCount,
                        }
                        );
                return query.ToList();
            }
        }

        public OrderProductDetail GetOrderProductById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .OrderProducts
                    .Single(e => e.OrderProductId == id);
                return
                new OrderProductDetail
                {
                    OrderId = entity.OrderId,
                    ProductId = entity.ProductId,
                    ProductCount = entity.ProductCount,
                    ProductName = entity.Product.ProductName
                };
            }
        }

        public string UpdateOrderProduct(int id, OrderProductEdit updatedOrderProduct)
        {
            using (var ctx = new ApplicationDbContext())
            {

                OrderProduct originalOrderProduct = ctx.OrderProducts.Find(id);

                if (originalOrderProduct.Order.IsOrderShipped)
                {
                    return "This order has been Shipped. You cannot make updates to it.";
                }

                Product originalProduct =
                    ctx
                    .Products
                    .Single(oP => oP.ProductId == originalOrderProduct.ProductId);

                Product updatedProduct =
                    ctx
                    .Products
                    .Single(uP => uP.ProductId == updatedOrderProduct.ProductId);

                if (originalOrderProduct.ProductId == updatedOrderProduct.ProductId)
                {
                    //check if enough original product is in inventory (current inventory + whatever was in the original order)
                    if (updatedOrderProduct.ProductCount >= originalProduct.UnitCount + originalOrderProduct.ProductCount)
                        return $"There is not enough inventory of {originalProduct.ProductName} available to update to this" +
                            $" OrderProduct's ProductCount. The current inventory (including the ProductCount on the original OrderProduct)" +
                            $" is {originalProduct.UnitCount + originalOrderProduct.ProductCount} units.";

                    originalProduct.UnitCount += originalOrderProduct.ProductCount; //return original request to inventory
                    originalProduct.UnitCount -= updatedOrderProduct.ProductCount; //remove current request from inventory

                    originalOrderProduct.OrderId = updatedOrderProduct.OrderId;
                    originalOrderProduct.ProductId = updatedOrderProduct.ProductId;
                    originalOrderProduct.ProductCount = updatedOrderProduct.ProductCount;

                    ctx.SaveChanges();

                    return $"The OrderProduct ID: {id} has been updated.";
                }

                else //if original orderproduct product is different from updated orderproduct product
                {
                    if (updatedOrderProduct.ProductCount >= updatedProduct.UnitCount)
                        return $"There is not enough inventory of the updated product: {updatedProduct.ProductName} available to update this OrderProduct's Product and ProductCount. The current {updatedProduct.ProductName} inventory is {updatedProduct.UnitCount} units."; //check if enough updated product is in inventory

                    originalProduct.UnitCount += originalOrderProduct.ProductCount; //return the original orderproduct to inventory (like you didn't mean to add to order)
                    updatedProduct.UnitCount -= updatedOrderProduct.ProductCount; //Subtract new orderproduct request from new product's inventory

                    originalOrderProduct.OrderId = updatedOrderProduct.OrderId;
                    originalOrderProduct.ProductId = updatedOrderProduct.ProductId;
                    originalOrderProduct.ProductCount = updatedOrderProduct.ProductCount;

                    ctx.SaveChanges();

                    return $"The OrderProduct ID: {id} has been updated with the updated product: {updatedProduct.ProductName}.";
                }
            }
        }

        public bool DeleteOrderProduct(int orderProductId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .OrderProducts
                    .Single(e => e.OrderProductId == orderProductId);

                var product =
                    ctx
                    .Products
                    .Single(p => p.ProductId == entity.ProductId);

                product.StockQuantity += entity.ProductCount; //return orderProduct to product inventory

                ctx.OrderProducts.Remove(entity);

                return ctx.SaveChanges() == 2;
            }
        }



    }
}

using Shorewind.Data;
using Shorewind.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Services
{
    public class ProductService
    {
        private readonly Guid _userId;

        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateProduct(ProductCreate model)
        {
            var entity =
                new Product()
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    CategoryId = model.CategoryId,
                    StockQuantity = model.StockQuantity,
                    UnitPrice = model.UnitPrice
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Products.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ProductListItem> GetProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Products
                    .Select(e => new ProductListItem
                    {
                        ProductId = e.ProductId,
                        ProductName = e.ProductName,
                        ProductDescription = e.ProductDescription,
                        CategoryId = e.CategoryId,
                        StockQuantity = e.StockQuantity,
                        UnitPrice = e.UnitPrice,
                        
                    });

                return query.ToArray();
            }
        }

        public ProductDetail GetProductById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                            .Single(e => e.ProductId == id);
                return
                    new ProductDetail
                    {
                        ProductId = entity.ProductId,
                        ProductName = entity.ProductName,
                        ProductDescription = entity.ProductDescription,
                        CategoryId = entity.CategoryId,
                        StockQuantity = entity.StockQuantity,
                        UnitPrice = entity.UnitPrice

                    };
            }
        }

        public bool UpdateProduct(ProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductId == model.ProductId);

                    entity.ProductId = model.ProductId;
                    entity.ProductName = model.ProductName;
                    entity.ProductDescription = model.ProductDescription;
                    entity.CategoryId = model.CategoryId;
                    entity.StockQuantity = model.StockQuantity;
                    entity.UnitPrice = model.UnitPrice;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductId == productId);

                ctx.Products.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}

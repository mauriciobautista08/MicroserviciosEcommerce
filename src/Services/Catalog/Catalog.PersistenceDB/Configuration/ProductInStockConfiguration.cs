using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalog.PersistenceDB.Configuration
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            var products = new List < ProductInStock>();
            var random = new Random();

            for(var i = 1; i <= 100; i++)
            {
                products.Add(new ProductInStock
                {
                    Id = i,
                    ProductID = i,
                    Stock = random.Next(0, 100)
                });
            }

            entityBuilder.HasData(products);
        }

    }
}

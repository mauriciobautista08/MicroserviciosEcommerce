using Catalog.Domain;
using Catalog.Service.Commons;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Test.config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Catalog.Service.EventHandlers.Commands.ProductInStockUpdateCommand;

namespace Catalog.Test
{
    [TestClass]
    public class ProductInStockUpdateStockEventHandler
    {

        private ILogger<ProductInStockUpdateEventHandler> GetLogger
        {
            get
            {
                return new Mock<ILogger<ProductInStockUpdateEventHandler>>().Object;
            }
        }

        [TestMethod]
        public async Task TryToSubstractStockWhenProductHasStock()
        {
            var context = ApplicationDBContextInMemory.Get();

            var productInStockId = 1;
            var productId = 1;

            context.Stock.Add(new ProductInStock
            {
                Id = productInStockId,
                ProductID = productId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = 1,
                        Stock = 1,
                        Action = Enums.ProductInStockAction.Subtract
                    }
                }
            }, new CancellationToken()).Wait();
        }
    }
}

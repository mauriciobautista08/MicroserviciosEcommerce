using Catalog.Domain;
using Catalog.Service.Commons;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Test.config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        [ExpectedException(typeof(ProductInStockUpdateCommandException))]
        public void TryToSubstractStockWhenProductHasntStock()
        {
            var context = ApplicationDBContextInMemory.Get();

            var productInStockID = 2;
            var productId = 2;

            context.Add(new ProductInStock
            {
                Id = productInStockID,
                ProductID = productId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateEventHandler(context, GetLogger);

            try
            {

                handler.Handle(new ProductInStockUpdateCommand
                {
                    Items = new List<ProductInStockUpdateItem>()
                    {
                        new ProductInStockUpdateItem
                        {
                            ProductId = productId,
                            Stock = 2,
                            Action = Enums.ProductInStockAction.Subtract
                        }
                    }
                }, new CancellationToken()).Wait();
            }
            catch (AggregateException ae)
            {
                if(ae.GetBaseException() is ProductInStockUpdateCommandException)
                {
                    throw new ProductInStockUpdateCommandException(ae.InnerException?.Message);
                }
            }
        }

        [TestMethod]
        public void TryToAddStockWhenProductExists()
        {
            var context = ApplicationDBContextInMemory.Get();

            var productInStockID = 3;
            var productId = 3;

            context.Add(new ProductInStock
            {
                Id = productInStockID,
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
                            ProductId = productId,
                            Stock = 2,
                            Action = Enums.ProductInStockAction.Add
                        }
                    }
            }, new CancellationToken()).Wait();

            Assert.AreEqual(context.Stock.First(x => x.Id == productInStockID).Stock, 3);
        }

        [TestMethod]
        public void TryToAddStockWhenProductNotExists()
        {
            var context = ApplicationDBContextInMemory.Get();

            var productInStockID = 4;
            var productId = 4;

            context.Add(new ProductInStock
            {
                Id = productInStockID,
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
                            ProductId = productId,
                            Stock = 2,
                            Action = Enums.ProductInStockAction.Add
                        }
                    }
            }, new CancellationToken()).Wait();

            Assert.AreEqual(context.Stock.First(x => x.Id == productInStockID).Stock, 3);
        }
    }
}

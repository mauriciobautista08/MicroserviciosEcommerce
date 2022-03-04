using Catalog.Domain;
using Catalog.PersistenceDB;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Catalog.Service.Commons.Enums;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateEventHandler : INotificationHandler<ProductInStockUpdateCommand>
    {

        private readonly ApplicationDBContext _context;
        private readonly ILogger<ProductInStockUpdateEventHandler> _logger;

        public ProductInStockUpdateEventHandler(ApplicationDBContext context, ILogger<ProductInStockUpdateEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ProductInStockUpdateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- Función de añadir/substraer producto en stock inicializada");
            var products = notification.Items.Select(x => x.ProductId);
            var stocks = await _context.Stock.Where(x => products.Contains(x.ProductID)).ToListAsync();

            _logger.LogInformation("--- Devolvió dato de la BD ---");

            foreach(var items in notification.Items)
            {
                var entry = stocks.SingleOrDefault(x => x.ProductID == items.ProductId);

                if(items.Action == ProductInStockAction.Subtract)
                {
                    if(entry == null || items.Stock > entry.Stock)
                    {
                        _logger.LogError($"--- el producto {entry.ProductID} no cuenta con suficiente stock");
                        throw new ProductInStockUpdateCommandException($" El producto {entry.ProductID} no cuenta con suficiente stock");
                    }

                    entry.Stock -= items.Stock;
                    _logger.LogInformation($"--- El stock del producto {entry.ProductID} ha sido subtraido con éxito, y su nuevo valor es {entry.Stock}");
                }
                else
                {
                    if(entry == null)
                    {
                        entry = new ProductInStock
                        {
                            ProductID = items.ProductId
                        };

                        _logger.LogInformation($" --- Nuevo registro de stock fue creado, porque el producto {entry.ProductID} no existía en la tabla de stocks");

                        await _context.AddAsync(entry);
                    }
                    _logger.LogInformation($"--- Se añadió stock al producto {entry.ProductID} --- ");
                    entry.Stock += items.Stock;

                }
            }

            await _context.SaveChangesAsync();
        }

    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler :
        INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICatalogProxy _catalogProxy;
        private readonly ILogger<OrderCreateEventHandler> _logger;

        public OrderCreateEventHandler(
            ApplicationDbContext context,
            ICatalogProxy catalogProxy,
            ILogger<OrderCreateEventHandler> logger)
        {
            _context = context;
            _catalogProxy = catalogProxy;
            _logger = logger;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- Creación de orden inicializada");
            var entry = new Domain.Order();

            using (var trx = await _context.Database.BeginTransactionAsync())
            {

                _logger.LogInformation("--- Preparando detalle");
                PrepareDetail(entry, notification);


                _logger.LogInformation("--- Preparando cabecera");
                PrepareHeader(entry, notification);


                _logger.LogInformation("--- Creando orden");
                await _context.AddAsync(entry);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"--- Orden {entry.OrderId} ha sido creada");


                _logger.LogInformation("--- Actualizando Stock");
                await _catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand
                {
                    Items = notification.Items.Select(x => new ProductInStockUpdateItem
                    {
                        ProductId = x.ProductId,
                        Stock = x.Quantity,
                        Action = ProductInStockAction.Substract
                    })
                });

                await trx.CommitAsync();
            }

            _logger.LogInformation("--- Creación de orden finalizada");
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.Price,
                Total = x.Price * x.Quantity
            }).ToList();
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            // Header information
            entry.Status = Common.Enums.OrderStatus.Pending;
            entry.PaymentType = notification.PaymentType;
            entry.ClientId = notification.ClientId;
            entry.CreatedAt = DateTime.UtcNow;

            // Sum
            entry.Total = entry.Items.Sum(x => x.Total);
        }
    }
}

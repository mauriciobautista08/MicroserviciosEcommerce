using Catalog.PersistenceDB;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateEventHandler : INotificationHandler<ProductInStockUpdateCommand>
    {

        private readonly ApplicationDBContext _context;

        public ProductInStockUpdateEventHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Handle(ProductInStockUpdateCommand notification, CancellationToken cancellationToken)
        {
            var products = notification.Items.Select(x => x.ProductId);
            var stocks = await _context.Stock.Where(x => products.Contains(x.ProductID)).ToListAsync();
        }

    }
}

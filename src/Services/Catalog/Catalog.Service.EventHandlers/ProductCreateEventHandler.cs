using Catalog.Domain;
using Catalog.PersistenceDB;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers
{
    public class ProductCreateEventHandler : INotificationHandler<ProductCreateCommnad>
    {
        private readonly ApplicationDBContext _context;

        public ProductCreateEventHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Handle(ProductCreateCommnad notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Product
            {
                Name = notification.Name,
                Description = notification.Description,
                Price = notification.Price
            });

            await _context.SaveChangesAsync();
        }

    }
}

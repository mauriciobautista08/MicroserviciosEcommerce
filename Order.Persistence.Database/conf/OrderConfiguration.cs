using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Persistence.Database.conf
{
    public class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<Domain.Order> entityBuilder)
        {
            entityBuilder.HasKey(x => x.OrderId);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Persistence.Database.conf
{
    public class OrderDetailConfiguration
    {
        public OrderDetailConfiguration(EntityTypeBuilder<OrderDetail> entityBuilder)
        {
            entityBuilder.HasKey(x => x.OrderDetailId);
        }
    }
}

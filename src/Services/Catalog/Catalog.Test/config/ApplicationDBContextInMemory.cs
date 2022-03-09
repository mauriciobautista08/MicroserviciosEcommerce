using Catalog.PersistenceDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Test.config
{
    public class ApplicationDBContextInMemory
    {
        public static ApplicationDBContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: $"Catalog.Db")
                .Options;

            return new ApplicationDBContext(options);
        }
    }
}

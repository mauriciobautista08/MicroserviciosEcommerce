using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain
{
    public class ProductInStock
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int Stock { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Service.Queries.DTOs
{
    public class ProductInStockDTO
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int Stock
        {
            get; set;
        }
    }
}

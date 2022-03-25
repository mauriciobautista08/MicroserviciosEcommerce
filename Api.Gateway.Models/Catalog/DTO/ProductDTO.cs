using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Gateway.Models.Catalog.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

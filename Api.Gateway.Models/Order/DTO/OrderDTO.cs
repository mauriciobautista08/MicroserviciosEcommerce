using Api.Gateway.Models.Catalog.DTO;
using Api.Gateway.Models.Customer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using static Api.Gateway.Models.Order.Commons.Enums;

namespace Api.Gateway.Models.Order.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public ClientDTO client { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<OrderDetailDto> items { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }

    }

    public class OrderDetailDto
    {
        public ProductDTO Product { get; set; }
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}

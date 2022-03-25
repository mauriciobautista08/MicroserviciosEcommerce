using System;
using System.Collections.Generic;
using System.Text;
using static Api.Gateway.Models.Order.Commons.Enums;

namespace Api.Gateway.Models.Order.Commands
{
    public class OrderCreateCommand
    {
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<OrderCreateDetail> Items { get; set; }

    }

    public class OrderCreateDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

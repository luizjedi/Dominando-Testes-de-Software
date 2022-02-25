using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdd_NerdStore.Core.Messages;

namespace Tdd_NerdStore.Application.Events
{
    public class AddedOrderItemEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitaryValue { get; private set; }
        public int Amount { get; private set; }

        public AddedOrderItemEvent(Guid clientId, Guid orderId, Guid productId, string productName, decimal unitaryValue, int amount)
        {
            this.ClientId = clientId;
            this.OrderId = orderId;
            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitaryValue = unitaryValue;
            this.Amount = amount;
        }
    }
}

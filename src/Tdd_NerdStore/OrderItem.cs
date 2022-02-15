using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdd_NerdStore.Domain
{
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitaryValue { get; private set; }

        public OrderItem(Guid productId, string productName, int amount, decimal unitaryValue)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Amount = amount;
            this.UnitaryValue = unitaryValue;
        }
    }
}

using System;
using Tdd_NerdStore.Core.DomainObjects;

namespace Tdd_NerdStore.Domain.Data
{
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitaryValue { get; private set; }

        public OrderItem(Guid productId, string productName, int amount, decimal unitaryValue)
        {
            if (amount < Order.Min_ITEM_UNITY)
                throw new DomainException($"Minimum of {Order.Min_ITEM_UNITY} units per product");

            this.ProductId = productId;
            this.ProductName = productName;
            this.Amount = amount;
            this.UnitaryValue = unitaryValue;
        }

        internal void AddUnity(int unity)
        {
            this.Amount += unity;
        }

        internal decimal CalculateTotalValue()
        {
            return this.Amount * this.UnitaryValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdd_NerdStore.Domain
{
    public class Order
    {
        public decimal TotalValue { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
            this._orderItems = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            _orderItems.Add(item);
            TotalValue = OrderItems.Sum(i => i.Amount * i.UnitaryValue);
        }
    }
}

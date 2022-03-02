using System;
using System.Threading.Tasks;
using Tdd_NerdStore.Core.Interfaces;
using Tdd_NerdStore.Domain.Data;

namespace Tdd_NerdStore.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order);
        void Update(Order order);
        Task<Order> GetOrderDraftByClientId(Guid clientId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
    }
}

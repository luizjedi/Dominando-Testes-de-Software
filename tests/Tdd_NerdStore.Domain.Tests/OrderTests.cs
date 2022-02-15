using System;
using Xunit;

namespace Tdd_NerdStore.Domain.Tests
{
    public class OrderTests
    {
        [Fact(DisplayName = "Add Void Order Item")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem_NewOrder_MustBeUpdateValue()
        {
            // Arranje 
            var order = new Order();
            var orderItem = new OrderItem(Guid.NewGuid(), "Product Test", 2, 100);

            // Act
            order.AddItem(orderItem);

            // Assert
            Assert.Equal(200, order.TotalValue);
        }
       
        
    }
}

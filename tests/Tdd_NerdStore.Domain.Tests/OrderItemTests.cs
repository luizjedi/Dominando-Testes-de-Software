using System;
using Tdd_NerdStore.Core.DomainObjects;
using Xunit;

namespace Tdd_NerdStore.Domain.Tests
{
    public class OrderItemTests
    {
        [Fact(DisplayName = "New Order Item Bellow Allowed")]
        [Trait("Category", "New Order Item Tests")]
        public void AddOrderItem_ItemBellowAllowed_MustBeReturnException()
        {
            // Arranje & Act & Assert
            Assert.Throws<DomainException>(() => new OrderItem(Guid.NewGuid(), "Test Product", Order.Min_ITEM_UNITY - 1, 100));
        }
    }
}

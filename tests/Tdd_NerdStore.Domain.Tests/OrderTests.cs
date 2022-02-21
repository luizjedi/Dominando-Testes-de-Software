using System;
using System.Linq;
using Tdd_NerdStore.Core.DomainObjects;
using Xunit;

namespace Tdd_NerdStore.Domain.Tests
{
    public class OrderTests
    {
        #region "Add"
        [Fact(DisplayName = "Add Void Order Item")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem_NewOrder_MustBeUpdateValue()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItem = new OrderItem(Guid.NewGuid(), "Product Test", 2, 100);

            // Act
            order.AddItem(orderItem);

            // Assert
            Assert.Equal(200, order.TotalValue);
        }

        [Fact(DisplayName = "Add Existing Order Item")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem_ExistingItem_MustBeIncreaseUnitySumValue()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 2, 100);
            order.AddItem(orderItem);

            var orderItem2 = new OrderItem(productId, "Test Product", 1, 100);

            // Act
            order.AddItem(orderItem2);

            // Assert
            Assert.Equal(300, order.TotalValue);
            Assert.Equal(1, order.OrderItems.Count);
            Assert.Equal(3, order.OrderItems.FirstOrDefault(p => p.ProductId == productId).Amount);
        }

        [Fact(DisplayName = "Add Order Item Over Allowed")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem_ItemOverAllowed_MustBeReturnException()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", Order.MAX_ITEM_UNITY + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.AddItem(orderItem));
        }

        [Fact(DisplayName = "Add Order Item Over Allowed To An Existing Product")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem_ItemExistingSumOfUnitsOverAllowed_MustBeReturnException()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 1, 100);
            var orderItem2 = new OrderItem(productId, "Test Product", Order.MAX_ITEM_UNITY, 100);
            order.AddItem(orderItem);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.AddItem(orderItem2));
        }
        #endregion

        #region "Update"
        [Fact(DisplayName = "Update Order Item Absent")]
        [Trait("Category", "Order Tests")]
        public void UpdateOrderItem_ItemDoesNotExistInTheList_MustBeReturnException()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItemUpdated = new OrderItem(Guid.NewGuid(), "Product Test", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.UpdateItem(orderItemUpdated));
        }

        [Fact(DisplayName = "Update Order Valid Item")]
        [Trait("Category", "Order Tests")]
        public void UpdateOrderItem_ValidItem_MustBeUpdateAmount()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Product Test", 2, 100);
            order.AddItem(orderItem);
            var orderItemUpdated = new OrderItem(productId, "Product Test", 5, 100);
            var newAmount = orderItemUpdated.Amount;

            // Act
            order.UpdateItem(orderItemUpdated);

            // Assert
            Assert.Equal(newAmount, order.OrderItems.FirstOrDefault(p => p.ProductId == productId).Amount);
        }

        [Fact(DisplayName = "Update Order Item Valid Total")]
        [Trait("Category", "Order Tests")]
        public void UpdateOrderItem_OrderWithOtherProducts_MustBeUpdateTotalValue()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(productId, "Product Test", 3, 15);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var orderItemUpdated = new OrderItem(productId, "Product Test", 5, 15);
            var totalOrder = (orderItem1.Amount * orderItem1.UnitaryValue) +
                             (orderItemUpdated.Amount * orderItemUpdated.UnitaryValue);

            // Act
            order.UpdateItem(orderItemUpdated);

            // Assert
            Assert.Equal(totalOrder, order.TotalValue);
        }

        [Fact(DisplayName = "Update Order Item Over Allowed")]
        [Trait("Category", "Order Tests")]
        public void UpdateOrderItem_OrderAmountProductsOverAllowed_MustBeReturnException()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Product Test", 1, 15);
            order.AddItem(orderItem);

            var orderItemUpdated = new OrderItem(productId, "Product Test", Order.MAX_ITEM_UNITY, 15);

            // Act &Assert
            Assert.Throws<DomainException>(() => order.UpdateItem(orderItemUpdated));
        }
        #endregion

        #region "Remove"
        [Fact(DisplayName = "Remove Order Item Absent")]
        [Trait("Category", "Order Tests")]
        public void RemoveOrderItem_ItemDoesNotExistInTheList_MustBeReturnException()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItemRemove = new OrderItem(Guid.NewGuid(), "Product Test", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.RemoveItem(orderItemRemove));
        }

        [Fact(DisplayName = "Remove Order Item Must Be Calculate Total Value")]
        [Trait("Category", "Order Tests")]
        public void RemoveOrderItem_ItemExisting_MustBeUpdateTotalValue()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 5, 100);
            var orderItem2 = new OrderItem(productId, "Product Test", 5, 200);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var totalOrder = orderItem2.Amount * orderItem2.UnitaryValue;

            // Act
            order.RemoveItem(orderItem1);

            // Assert
            Assert.Equal(totalOrder, order.TotalValue);
        }
        #endregion

        #region "Voucher"
        [Fact(DisplayName = "Apply Valid Voucher")]
        [Trait("Category", "Order Tests")]
        public void Order_ApplyValidVoucher_MustReturnWithoutErrors()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var voucher = new Voucher(
                code: "PROMO-15-REAIS",
                discountValue: 15,
                discountPercent: null,
                amount: 1,
                expirationDate: DateTime.Now.AddDays(15),
                voucherDiscountType: VoucherDiscountType.Value,
                active: true,
                used: false);

            // Act
            var result = order.ApplyVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Apply Invalid Voucher")]
        [Trait("Category", "Order Tests")]
        public void Order_ApplyInvalidVoucher_MustReturnWithErrors()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var voucher = new Voucher(
                code: "PROMO-15-REAIS",
                discountValue: 15,
                discountPercent: null,
                amount: 1,
                expirationDate: DateTime.Now.AddDays(-1),
                voucherDiscountType: VoucherDiscountType.Value,
                active: true,
                used: true);

            // Act
            var result = order.ApplyVoucher(voucher);

            // Assert
            Assert.False(result.IsValid);
        }


        [Fact(DisplayName = "Apply Discount Value Type Voucher ")]
        [Trait("Category", "Order Tests")]
        public void Order_VoucherDiscountValueType_MustDiscountOfTotalValue()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());

            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(Guid.NewGuid(), "Product Test", 3, 15);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var voucher = new Voucher(
                code: "PROMO-15-REAIS",
                discountValue: 15,
                discountPercent: null,
                amount: 1,
                expirationDate: DateTime.Now.AddDays(10),
                voucherDiscountType: VoucherDiscountType.Value,
                active: true,
                used: false);

            var valueWithDiscount = order.TotalValue - voucher.DiscountValue;

            // Act
            order.ApplyVoucher(voucher);

            // Assert
            Assert.Equal(valueWithDiscount, order.TotalValue);
        }

        [Fact(DisplayName = "Apply Discount Percentage Type Voucher ")]
        [Trait("Category", "Order Tests")]
        public void Order_VoucherDiscountPercentageType_MustDiscountOfTotalValue()
        {
            // Arranje 
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());

            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(Guid.NewGuid(), "Product Test", 3, 15);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var voucher = new Voucher(
                code: "PROMO-15-REAIS",
                discountValue: null,
                discountPercent: 15,
                amount: 1,
                expirationDate: DateTime.Now.AddDays(10),
                voucherDiscountType: VoucherDiscountType.Percentage,
                active: true,
                used: false);

            var valueWithDiscount = order.TotalValue - (order.TotalValue * (voucher.DiscountPercent / 100));

            // Act
            order.ApplyVoucher(voucher);

            // Assert
            Assert.Equal(valueWithDiscount, order.TotalValue);
        }
        #endregion
    }
}

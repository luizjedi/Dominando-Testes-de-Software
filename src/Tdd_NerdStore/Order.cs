using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using Tdd_NerdStore.Core.DomainObjects;

namespace Tdd_NerdStore.Domain
{
    public class Order
    {
        #region "Properties"
        public static int MAX_ITEM_UNITY => 15;
        public static int Min_ITEM_UNITY => 1;

        public Guid ClientId { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal Discount { get; private set; }

        public bool UsedVoucher { get; private set; }
        public Voucher Voucher { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        #endregion

        #region "Methods"
        protected Order()
        {
            this._orderItems = new List<OrderItem>();
        }
        private void CalculateOrderValue()
        {
            this.TotalValue = OrderItems.Sum(i => i.CalculateTotalValue());
        }
        public void MakeDraft()
        {
            this.OrderStatus = OrderStatus.Draft;
        }
        public bool OrderItemExisting(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }
        public void ValidateOrderItemAbsent(OrderItem item)
        {
            if (!this.OrderItemExisting(item))
                throw new DomainException($"The Item Does Not Exist In The Order");
        }
        public void ValidateAmountItemAllowed(OrderItem item)
        {
            var amountItems = item.Amount;

            if (this.OrderItemExisting(item))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                amountItems += existingItem.Amount;
            }

            if (amountItems > MAX_ITEM_UNITY)
                throw new DomainException($"Maximum of {MAX_ITEM_UNITY} units per product");
        }
        public void AddItem(OrderItem item)
        {
            this.ValidateAmountItemAllowed(item);

            if (this.OrderItemExisting(item))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

                existingItem.AddUnity(item.Amount);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            _orderItems.Add(item);
            this.CalculateOrderValue();
        }
        public void UpdateItem(OrderItem item)
        {
            this.ValidateOrderItemAbsent(item);
            ValidateAmountItemAllowed(item);

            var existingItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            _orderItems.Remove(existingItem);
            _orderItems.Add(item);
            this.CalculateOrderValue();
        }
        public void RemoveItem(OrderItem item)
        {
            this.ValidateOrderItemAbsent(item);

            _orderItems.Remove(item);

            this.CalculateOrderValue();
        }
        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var result = voucher.VoucherIsValid();

            if (!result.IsValid) return result;

            this.Voucher = voucher;
            this.UsedVoucher = true;

            this.CalculateTotalValueDiscount();

            return result;
        }
        public void CalculateTotalValueDiscount()
        {
            if (!this.UsedVoucher) return;

            decimal discount = 0;

            if (this.Voucher.VoucherDiscountType == VoucherDiscountType.Value)
            {
                if (this.Voucher.DiscountValue.HasValue)
                    discount = this.Voucher.DiscountValue.Value;
            }
            else if (this.Voucher.DiscountPercent.HasValue)
                discount = (this.TotalValue * (this.Voucher.DiscountPercent / 100)).Value;

            this.Discount = discount;
            this.TotalValue -= this.Discount;
        }
        #endregion

        #region "Child Class"
        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId,
                };

                order.MakeDraft();
                return order;
            }
        }
        #endregion
    }
}

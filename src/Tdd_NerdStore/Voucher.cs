using FluentValidation.Results;
using System;

namespace Tdd_NerdStore.Domain
{
    public class Voucher
    {
        #region "Properties"
        public string Code { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public decimal? DiscountPercent { get; private set; }
        public int Amount { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        public VoucherDiscountType? VoucherDiscountType { get; private set; }
        #endregion

        public Voucher(string code, decimal? discountValue, decimal? discountPercent, int amount, DateTime expirationDate,
            VoucherDiscountType? voucherDiscountType, bool active, bool used)
        {
            this.Code = code;
            this.DiscountValue = discountValue;
            this.DiscountPercent = discountPercent;
            this.Amount = amount;
            this.ExpirationDate = expirationDate;
            this.VoucherDiscountType = voucherDiscountType;
            this.Active = active;
            this.Used = used;
        }

        #region "Methods"
        public ValidationResult VoucherIsValid()
        {
            return new VoucherApplicableValidation().Validate(this);
        }
        #endregion

    }
}

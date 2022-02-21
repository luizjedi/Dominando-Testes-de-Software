using System;
using System.Linq;
using Xunit;

namespace Tdd_NerdStore.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validate Voucher Type Valid Value")]
        [Trait("Category", "Voucher Tests")]
        public void Voucher_ValidateVoucherTypeValue_MustBeValid()
        {
            // Arranje 
            var voucher = new Voucher(
                code: "PROMO-15-REAIS",
                discountValue: 150,
                discountPercent: null,
                amount: 1,
                expirationDate: DateTime.Now.AddDays(7),
                voucherDiscountType: VoucherDiscountType.Value,
                active: true,
                used: false);

            // Act
            var result = voucher.VoucherIsValid();

            // Assert
            Assert.True(result.IsValid);

        }

        [Fact(DisplayName = "Validate Voucher Type Invalid Value")]
        [Trait("Category", "Voucher Tests")]
        public void Voucher_ValidateVoucherTypeValue_MustBeInvalid()
        {
            // Arranje 
            var voucher = new Voucher(
                code: "",
                discountValue: null,
                discountPercent: null,
                amount: 0,
                expirationDate: DateTime.Now.AddDays(-1),
                voucherDiscountType: VoucherDiscountType.Value,
                active: false,
                used: true);

            // Act
            var result = voucher.VoucherIsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherApplicableValidation.ActiveErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.CodeErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.DiscountValuErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.AmountErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.ExpirationDateErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.UsedErrorMg, result.Errors.Select(e => e.ErrorMessage));

        }

        [Fact(DisplayName = "Validate Voucher Type Valid Percentage")]
        [Trait("Category", "Voucher Tests")]
        public void Voucher_ValidateVoucherTypePercentage_MustBeValid()
        {
            // Arranje 
            var voucher = new Voucher(
                code: "PROMO-15-REAIS",
                discountValue: null,
                discountPercent: 70,
                amount: 1,
                expirationDate: DateTime.Now.AddDays(7),
                voucherDiscountType: VoucherDiscountType.Percentage,
                active: true,
                used: false);

            // Act
            var result = voucher.VoucherIsValid();

            // Assert
            Assert.True(result.IsValid);

        }

        [Fact(DisplayName = "Validate Voucher Type Invalid Percentage")]
        [Trait("Category", "Voucher Tests")]
        public void Voucher_ValidateVoucherTypePercentage_MustBeInvalid()
        {
            // Arranje 
            var voucher = new Voucher(
                code: "",
                discountValue: null,
                discountPercent: null,
                amount: 0,
                expirationDate: DateTime.Now.AddDays(-1),
                voucherDiscountType: VoucherDiscountType.Percentage,
                active: false,
                used: true);

            // Act
            var result = voucher.VoucherIsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherApplicableValidation.ActiveErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.CodeErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.DiscountPercentErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.AmountErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.ExpirationDateErrorMg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.UsedErrorMg, result.Errors.Select(e => e.ErrorMessage));

        }
    }
}

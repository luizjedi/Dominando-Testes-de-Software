using FluentValidation;
using System;
using Tdd_NerdStore.Domain.Enums;

namespace Tdd_NerdStore.Domain.Data
{
    public class VoucherApplicableValidation : AbstractValidator<Voucher>
    {
        public static string CodeErrorMg => "Voucher without valid code.";
        public static string ExpirationDateErrorMg => "This voucher has expired.";
        public static string ActiveErrorMg => "This voucher is not valid.";
        public static string UsedErrorMg => "This voucher has been used.";
        public static string AmountErrorMg => "This voucher is no longer available.";
        public static string DiscountValuErrorMg => "The discount must be over 0.";
        public static string DiscountPercentErrorMg => "The percentage value of discount must be over 0.";

        public VoucherApplicableValidation()
        {
            RuleFor(p => p.Code)
                .NotEmpty()
                .WithMessage(CodeErrorMg);

            RuleFor(p => p.ExpirationDate)
                .Must(ExpirationDateOverActual)
                .WithMessage(ExpirationDateErrorMg);

            RuleFor(p => p.Active)
                .Equal(true)
                .WithMessage(ActiveErrorMg);

            RuleFor(p => p.Used)
                .Equal(false)
                .WithMessage(UsedErrorMg);

            RuleFor(p => p.Amount)
                .GreaterThan(0)
                .WithMessage(AmountErrorMg);

            When(p => p.VoucherDiscountType == VoucherDiscountType.Value, () =>
            {
                RuleFor(p => p.DiscountValue)
                    .NotNull()
                    .WithMessage(DiscountValuErrorMg)
                    .GreaterThan(0)
                    .WithMessage(DiscountValuErrorMg);
            });

            When(p => p.VoucherDiscountType == VoucherDiscountType.Percentage, () =>
            {
                RuleFor(p => p.DiscountPercent)
                .NotNull()
                .WithMessage(DiscountPercentErrorMg)
                .GreaterThan(0)
                .WithMessage(DiscountPercentErrorMg);
            });
        }

        protected static bool ExpirationDateOverActual(DateTime? expirationDate)
        {
            return expirationDate >= DateTime.Now;
        }
    }
}

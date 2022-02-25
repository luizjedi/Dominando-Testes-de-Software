using FluentValidation;
using System;
using Tdd_NerdStore.Domain.Data;

namespace Tdd_NerdStore.Application.Commands
{
    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public static string ClientIdErrorMsg => "Invalid client id.";
        public static string ProductIdErrorMsg => "Invalid product id.";
        public static string ProductNameErrorMsg => "Product name not specified.";
        public static string AmountMaxErrorMsg => $"The maximum amount of the item is {Order.MAX_ITEM_UNITY}.";
        public static string AmountMinErrorMsg => $"The minimum amount of the item is {Order.Min_ITEM_UNITY}.";
        public static string UnitaryValueErrorMsg => "The item value must be over 0.";

        public AddOrderItemValidation()
        {
            RuleFor(p => p.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage(ClientIdErrorMsg);

            RuleFor(p => p.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage(ProductIdErrorMsg);

            RuleFor(p => p.ProductName)
                .NotEmpty()
                .WithMessage(ProductNameErrorMsg);

            RuleFor(p => p.Amount)
                .GreaterThan(0)
                .WithMessage(AmountMinErrorMsg)
                .LessThanOrEqualTo(Order.MAX_ITEM_UNITY)
                .WithMessage(AmountMaxErrorMsg);

            RuleFor(p => p.UnitaryValue)
                .GreaterThan(0)
                .WithMessage(UnitaryValueErrorMsg);
        }
    }
}

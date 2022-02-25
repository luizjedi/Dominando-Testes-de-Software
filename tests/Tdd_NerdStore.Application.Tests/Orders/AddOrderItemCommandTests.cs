using System;
using System.Linq;
using Tdd_NerdStore.Application.Commands;
using Tdd_NerdStore.Domain;
using Tdd_NerdStore.Domain.Data;
using Xunit;

namespace Tdd_NerdStore.Application.Tests.Orders
{
    public class AddOrderItemCommandTests
    {
        [Fact(DisplayName = "Add Item Command Valid")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddOrderItemCommand_CommandItIsValid_MustPassValidation()
        {
            // Arranje 
            var orderCommand = new AddOrderItemCommand(
                clientId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                productName: "Product Test",
                amount: 2,
                unitaryValue: 100);

            // Act
            var result = orderCommand.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Add Item Command Invalid - Min Value")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddOrderItemCommand_CommandItemIsInvalid_MustNotPassValidation()
        {
            // Arranje 
            var orderCommand = new AddOrderItemCommand(
                clientId: Guid.Empty,
                productId: Guid.Empty,
                productName: "",
                amount: Order.Min_ITEM_UNITY - 1,
                unitaryValue: 0);

            // Act
            var result = orderCommand.IsValid();

            // Assert
            Assert.False(result);
            Assert.Contains(AddOrderItemValidation.ClientIdErrorMsg, orderCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AddOrderItemValidation.ProductIdErrorMsg, orderCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AddOrderItemValidation.ProductNameErrorMsg, orderCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AddOrderItemValidation.AmountMinErrorMsg, orderCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AddOrderItemValidation.UnitaryValueErrorMsg, orderCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "Add Item Command Invalid - Max Value")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddOrderItemCommand_CommandMaxItemIsInvalid_MustNotPassValidation()
        {
            // Arranje 
            var orderCommand = new AddOrderItemCommand(
                clientId: Guid.Empty,
                productId: Guid.Empty,
                productName: "Product Test",
                amount: Order.MAX_ITEM_UNITY + 1,
                unitaryValue: 100);

            // Act
            var result = orderCommand.IsValid();

            // Assert
            Assert.False(result);
            Assert.Contains(AddOrderItemValidation.AmountMaxErrorMsg, orderCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}

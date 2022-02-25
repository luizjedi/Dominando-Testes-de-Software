using System;
using Tdd_NerdStore.Core.Messages;

namespace Tdd_NerdStore.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        #region "Properties"
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Amount { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitaryValue { get; private set; }
        #endregion

        public AddOrderItemCommand(Guid clientId, Guid productId, int amount, string productName, decimal unitaryValue)
        {
            ClientId = clientId;
            ProductId = productId;
            Amount = amount;
            ProductName = productName;
            UnitaryValue = unitaryValue;
        }

        #region "Methods"
        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        #endregion

    }
}


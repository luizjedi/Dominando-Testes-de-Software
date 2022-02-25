using FluentValidation.Results;
using MediatR;
using System;

namespace Tdd_NerdStore.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            this.Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}

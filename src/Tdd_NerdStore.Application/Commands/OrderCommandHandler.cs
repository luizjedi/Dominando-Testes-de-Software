using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tdd_NerdStore.Application.Events;
using Tdd_NerdStore.Core.DomainObjects;
using Tdd_NerdStore.Core.Messages;
using Tdd_NerdStore.Domain.Data;
using Tdd_NerdStore.Domain.Interfaces;

namespace Tdd_NerdStore.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            this._orderRepository = orderRepository;
            this._mediator = mediator;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!this.ValidateCommand(message)) return false;

            var order = await this._orderRepository.GetOrderDraftByClientId(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.ProductName, message.Amount, message.UnitaryValue);

            if (order == null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItem(orderItem);
                this._orderRepository.Add(order);
            }
            else
            {
                var orderItemExisting = order.OrderItemExisting(orderItem);
                order.AddItem(orderItem);

                if (orderItemExisting)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(o => o.ProductId == orderItem.ProductId));
                }
                else
                {
                    this._orderRepository.AddItem(orderItem);
                }

                this._orderRepository.Update(order);
            }

            order.AddEvent(new AddedOrderItemEvent(order.ClientId, order.Id, message.ProductId,
                message.ProductName, message.UnitaryValue, message.Amount));

            return await this._orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.Publish(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}

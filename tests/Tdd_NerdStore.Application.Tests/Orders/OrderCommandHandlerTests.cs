using MediatR;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tdd_NerdStore.Application.Commands;
using Tdd_NerdStore.Domain.Data;
using Tdd_NerdStore.Domain.Interfaces;
using Xunit;

namespace Tdd_NerdStore.Application.Tests.Orders
{
    public class OrderCommandHandlerTests
    {

        [Fact(DisplayName = "Add New Order Item With Success")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem_NewOrder_MustRunWithSuccess()
        {
            // Arranje 
            var orderCommand = new AddOrderItemCommand(
                clientId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                productName: "Product Test",
                amount: 2,
                unitaryValue: 100);

            var mocker = new AutoMocker();
            var orderHandler = mocker.CreateInstance<OrderCommandHandler>();

            mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await orderHandler.Handle(orderCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            mocker.GetMock<IOrderRepository>().Verify(r => r.Add(It.IsAny<Order>()), Times.Once);
            mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
            //mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add New Order Item Draft With Success")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem_NewOrderItemDraft_MustRunWithSuccess()
        {
            // Arranje 
            var clientId = Guid.NewGuid();

            var order = Order.OrderFactory.NewOrderDraft(clientId);
            var orderItemExisting = new OrderItem(productId: Guid.NewGuid(), productName: "Product Xpto", amount: 2, unitaryValue: 100);
            order.AddItem(orderItemExisting);

            var orderCommand = new AddOrderItemCommand(clientId: clientId, productId: Guid.NewGuid(),
                productName: "Product Test", amount: 2, unitaryValue: 100);

            var mocker = new AutoMocker();
            var orderHandler = mocker.CreateInstance<OrderCommandHandler>();

            mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetOrderDraftByClientId(clientId)).Returns(Task.FromResult(order));
            mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await orderHandler.Handle(orderCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            mocker.GetMock<IOrderRepository>().Verify(r => r.AddItem(It.IsAny<OrderItem>()), Times.Once);
            mocker.GetMock<IOrderRepository>().Verify(r => r.Update(It.IsAny<Order>()), Times.Once);
            mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

    }
}

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
        private readonly Guid _clientId;
        private readonly Guid _productId;
        private readonly Order _order;
        private readonly AutoMocker _mocker;
        private readonly OrderCommandHandler _orderHandler;

        public OrderCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _orderHandler = _mocker.CreateInstance<OrderCommandHandler>();

            _clientId = Guid.NewGuid();
            _productId = Guid.NewGuid();

            _order = Order.OrderFactory.NewOrderDraft(_clientId);
        }

        [Fact(DisplayName = "Add New Order Item With Success")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem_NewOrder_MustRunWithSuccess()
        {
            // Arranje 
            var orderCommand = new AddOrderItemCommand(
                clientId: _clientId,
                productId: _productId,
                productName: "Product Test",
                amount: 2,
                unitaryValue: 100);

            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _orderHandler.Handle(orderCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Add(It.IsAny<Order>()), Times.Once);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
            //mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add New Order Item Draft With Success")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem_NewOrderItemDraft_MustRunWithSuccess()
        {
            // Arranje 
            var orderItemExisting = new OrderItem(productId: Guid.NewGuid(), productName: "Product Xpto", amount: 2, unitaryValue: 100);
            _order.AddItem(orderItemExisting);

            var orderCommand = new AddOrderItemCommand(clientId: _clientId, productId: Guid.NewGuid(),
                productName: "Product Test", amount: 2, unitaryValue: 100);

            _mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetOrderDraftByClientId(_clientId)).Returns(Task.FromResult(_order));
            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _orderHandler.Handle(orderCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.AddItem(It.IsAny<OrderItem>()), Times.Once);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Update(It.IsAny<Order>()), Times.Once);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Add Existing Item To The Order Draft With Success")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem_ExixtingItemToTheOrderDraft_MustRunWithSuccess()
        {
            // Arranje 
            var orderItemExisting = new OrderItem(productId: _productId, productName: "Product Xpto", amount: 2, unitaryValue: 100);
            _order.AddItem(orderItemExisting);

            var orderCommand = new AddOrderItemCommand(
                clientId: _clientId, productId: _productId, productName: "Product Xpto", amount: 2, unitaryValue: 100);

            _mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetOrderDraftByClientId(_clientId)).Returns(Task.FromResult(_order));
            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _orderHandler.Handle(orderCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UpdateItem(It.IsAny<OrderItem>()), Times.Once);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Update(It.IsAny<Order>()), Times.Once);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Add Item Command Invalid")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem_CommandInvalid_MustReturnFalseAndThrowNotificationEvents()
        {
            // Arranje 
            var orderCommand = new AddOrderItemCommand(
                clientId: _clientId, productId: _productId, productName: "", amount: 0, unitaryValue: 0);

            // Act
            var result = await _orderHandler.Handle(orderCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(3));
        }
    }
}

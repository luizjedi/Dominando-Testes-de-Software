using Features.Clients;
using Features.Tests._06_AutoMock;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using System.Threading;
using Xunit;

namespace Features.Tests._07_FluentAssertions
{
    [Collection(nameof(ClientAutoMockerCollection))]
    public class ClientServiceFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clientServiceAutoMockerFixture;
        private readonly ClientService _clientService;
        public ClientServiceFluentAssertionsTests(ClientTestsAutoMockerFixture clientService)
        {
            this._clientServiceAutoMockerFixture = clientService;
            this._clientService = _clientServiceAutoMockerFixture.GetClientService();
        }

        [Fact(DisplayName = "Add Client with success")]
        [Trait("Category", "Client Service Fluent Assertion Tests")]
        public void ClientService_Add_MustRunWithSuccess()
        {
            // Arrange 
            var client = _clientServiceAutoMockerFixture.GenerateValidClient();

            // Act 
            _clientService.Add(client);

            // Assert
            //Assert.True(client.IsValid());

            // Assert
            client.IsValid().Should().BeTrue();
            _clientServiceAutoMockerFixture.Mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Once); // Times checa quantas vezes o método é chamado.
            _clientServiceAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client with fail")]
        [Trait("Category", "Client Service Fluent Assertion Tests")]
        public void ClientService_Add_MustRunWithFail()
        {
            // Arrange 
            var client = _clientServiceAutoMockerFixture.GenerateInvalidClient();

            // Act 
            _clientService.Add(client);

            // Assert
            //Assert.False(client.IsValid());

            // Assert
            client.IsValid().Should().BeFalse();
            _clientServiceAutoMockerFixture.Mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Never); // Times checa quantas vezes o método é chamado.
            _clientServiceAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Get Active Client")]
        [Trait("Category", "Client Service Fluent Assertion Tests")]
        public void ClientService_GetAllActives_ShouldReturnOnlyActiveClients()
        {
            // Arrange
            _clientServiceAutoMockerFixture.Mocker.GetMock<IClientRepository>()
                .Setup(c => c.GetAll())
                .Returns(_clientServiceAutoMockerFixture.GenerateVariedClient());

            // Act 
            var clients = _clientService.GetAllActives();

            // Assert
            //Assert.True(clients.Any());
            //Assert.False(clients.Count(c => !c.Active) > 0);

            // Assert
            clients.Should().HaveCountGreaterThanOrEqualTo(1).And.OnlyHaveUniqueItems();
            clients.Should().NotContain(c => !c.Active);
            _clientServiceAutoMockerFixture.Mocker.GetMock<IClientRepository>().Verify(r => r.GetAll(), Times.Once); // Times checa quantas vezes o método é chamado.
           
            // Asserção mais recomendada para a utilização em teste de integração
            //_clientService.ExecutionTimeOf(c => c.GetAllActives())
            //    .Should()
            //    .BeLessOrEqualTo(50.Milliseconds(), "runs thousands of times");
        }
    }
}

using Features.Clients;
using Features.Tests._04_Human_Data;
using MediatR;
using Moq;
using Moq.AutoMock;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests._06_AutoMock
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceAutoMockerTests
    {
        private readonly ClientTestsBogusFixture _clientTestsBogus;

        public ClientServiceAutoMockerTests(ClientTestsBogusFixture clientTestsBogus)
        {
            this._clientTestsBogus = clientTestsBogus;
        }

        [Fact(DisplayName = "Add Client with success")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_Add_MustRunWithSuccess()
        {
            // Arrange 
            var client = _clientTestsBogus.GenerateValidClient();
            var mocker = new AutoMocker();

            var clientService = mocker.CreateInstance<ClientService>();
            // Act 
            clientService.Add(client);

            // Assert
            Assert.True(client.IsValid());
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Once); // Times checa quantas vezes o método é chamado.
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client with fail")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_Add_MustRunWithFail()
        {
            // Arrange 
            var client = _clientTestsBogus.GenerateInvalidClient();
            var mocker = new AutoMocker();

            var clientService = mocker.CreateInstance<ClientService>();

            // Act 
            clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Never); // Times checa quantas vezes o método é chamado.
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Get Active Client")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_GetAllActives_ShouldReturnOnlyActiveClients()
        {
            // Arrange
            var mocker = new AutoMocker();
            var clientService = mocker.CreateInstance<ClientService>();

            mocker.GetMock<IClientRepository>()
                .Setup(c => c.GetAll())
                .Returns(_clientTestsBogus.GenerateVariedClient());

            // Act 
            var clients = clientService.GetAllActives();

            // Assert
            mocker.GetMock<IClientRepository>().Verify(r => r.GetAll(), Times.Once); // Times checa quantas vezes o método é chamado.
            Assert.True(clients.Any());
            Assert.False(clients.Count(c => !c.Active) > 0);
        }
    }
}

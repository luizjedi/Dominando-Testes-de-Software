using Features.Clients;
using Features.Tests._04_Human_Data;
using MediatR;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests._05_Mock
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceTests
    {
        private readonly ClientTestsBogusFixture _clientTestsBogus;

        public ClientServiceTests(ClientTestsBogusFixture clientTestsBogus)
        {
            this._clientTestsBogus = clientTestsBogus;
        }

        [Fact(DisplayName = "Add Client with success")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustRunWithSuccess()
        {
            // Arrange 
            var client = _clientTestsBogus.GenerateValidClient();
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            // Act 
            clientService.Add(client);

            // Assert
            Assert.True(client.IsValid());
            clientRepo.Verify(r => r.Add(client), Times.Once); // Times checa quantas vezes o método é chamado.
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client with fail")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustRunWithFail()
        {
            // Arrange 
            var client = _clientTestsBogus.GenerateInvalidClient();
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            // Act 
            clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            clientRepo.Verify(r => r.Add(client), Times.Never); // Times checa quantas vezes o método é chamado.
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Get Active Client")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_GetAllActives_ShouldReturnOnlyActiveClients()
        {
            // Arrange
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            clientRepo
                .Setup(c => c.GetAll())
                .Returns(_clientTestsBogus.GenerateVariedClient());

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            // Act 
           var clients = clientService.GetAllActives();

            // Assert
            clientRepo.Verify(r => r.GetAll(), Times.Once); // Times checa quantas vezes o método é chamado.
            Assert.True(clients.Any());
            Assert.False(clients.Count(c => !c.Active) > 0);
        }
    }
}

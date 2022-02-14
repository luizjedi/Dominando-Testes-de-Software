using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Clients
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;

        public ClientService(IClientRepository clientRepository, IMediator mediator)
        {
            this._clientRepository = clientRepository;
            this._mediator = mediator;
        }

        public IEnumerable<Client> GetAllActives()
        {
            return _clientRepository.GetAll().Where(c => c.Active);
        }

        public void Add(Client client)
        {
            if (!client.IsValid())
                return;

            _clientRepository.Add(client);
            _mediator.Publish(new ClientEmailNotification(origin: "admin@me.com", destiny: client.Email, subject: "Hello", message: "Welcome!"));
        }

        public void Update(Client client)
        {
            if (!client.IsValid())
                return;

            _clientRepository.Update(client);
            _mediator.Publish(new ClientEmailNotification(origin: "admin@me.com", destiny: client.Email, "Changes", "Get a Look!"));
        }

        public void InactivateOrActivate(Client client)
        {
            if (!client.IsValid())
                return;

            client.InactivateOrActivate();
            _clientRepository.Update(client);
            _mediator.Publish(new ClientEmailNotification(origin: "admin@me.com", destiny: client.Email, "Up to Soon", "Até mais tarde!"));
        }

        public void Remove(Client client)
        {
            _clientRepository.Remove(client.Id);
            _mediator.Publish(new ClientEmailNotification(origin: "admin@me.com", destiny: client.Email, "Bye", "Have a amazing journey!"));
        }

        public void Dispose()
        {
            _clientRepository.Dispose();
        }
    }
}
using Bogus;
using Bogus.DataSets;
using Features.Clients;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Features.Tests._06_AutoMock
{
    public class ClientTestsAutoMockerFixture : IDisposable
    {
        public ClientService ClientService;
        public AutoMocker Mocker;

        public ClientService GetClientService()
        {
            Mocker = new AutoMocker();
            ClientService = Mocker.CreateInstance<ClientService>();

            return ClientService;
        }

        public IEnumerable<Client> GenerateClients(int amount, bool active)
        {
            var gender = new Faker().PickRandom<Name.Gender>();
            //var email = new Faker("pt-BR").Internet.Email("luiz");

            var clients = new Faker<Client>("pt_BR")
                    .CustomInstantiator(f => new Client(
                        Guid.NewGuid(),
                        name: f.Name.FirstName(gender),
                        lastName: f.Name.LastName(gender),
                        birthDate: f.Date.Past(80, DateTime.Now.AddYears(-18)),
                        email: "",
                        active: active,
                        registerDate: f.Date.Past(80)))

                    .RuleFor(f => f.Email, (f, c) => f.Internet.Email(c.Name.ToLower(), c.LastName.ToLower()));

            return clients.Generate(amount);
        }
        public IEnumerable<Client> GenerateVariedClient()
        {
            var clients = new List<Client>();

            clients.AddRange(GenerateClients(50, true).ToList());
            clients.AddRange(GenerateClients(50, false).ToList());

            return clients;
        }

        public Client GenerateValidClient()
        {
            return GenerateClients(1, true).FirstOrDefault();
        }

        public Client GenerateInvalidClient()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Client>("pt_BR")
                .CustomInstantiator(f => new Client(
                    Guid.NewGuid(),
                    name: f.Name.FirstName(gender),
                    lastName: f.Name.LastName(gender),
                    birthDate: f.Date.Past(1, DateTime.Now.AddYears(1)),
                    email: "",
                    active: false,
                    registerDate: DateTime.Now));

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}

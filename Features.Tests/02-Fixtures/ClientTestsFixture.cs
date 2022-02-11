using Bogus;
using Bogus.DataSets;
using Features.Clients;
using System;
using Xunit;

namespace Features.Tests._02_Fixtures
{
    public class ClientTestsFixture : IDisposable
    {
        public Client GenerateValidClient()
        {
            var gender = new Faker().PickRandom<Name.Gender>();
            //var email = new Faker("pt-BR").Internet.Email("luiz");

            var client = new Faker<Client>("pt_BR")
                    .CustomInstantiator(f => new Client(
                        Guid.NewGuid(),
                        name: f.Name.FirstName(gender),
                        lastName: f.Name.LastName(gender),
                        birthDate: f.Date.Past(80, DateTime.Now.AddYears(-18)),
                        email: "",
                        active: true,
                        registerDate: f.Date.Past(80)))
                    
                    .RuleFor(f => f.Email, (f, c) => f.Internet.Email(c.Name.ToLower(), c.LastName.ToLower()));

            return client;
        }


        public Client GenerateInValidClient()
        {
            var client = new Client(
                Guid.NewGuid(),
                name: "",
                lastName: "",
                birthDate: DateTime.Now,
                email: "luiz.pepe2hotmail.com",
                active: true,
                registerDate: DateTime.Now);

            return client;
        }

        public void Dispose()
        {
        }
    }
}

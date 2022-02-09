﻿using Bogus;
using Bogus.DataSets;
using Features.Clients;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Features.Tests._04_Human_Data
{
    public class ClientTestsBogusFixture : IDisposable
    {
        public IEnumerable<Client> GenerateClients(int amount, bool active)
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
                        active: active,
                        registerDate: f.Date.Past(80)))

                    .RuleFor(f => f.Email, (f, c) => f.Internet.Email(c.Name.ToLower(), c.LastName.ToLower()));

            return client.Generate(amount);
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

        public IEnumerable<Client> ObterClientsVariados()
        {
            var clientes = new List<Client>();

            clientes.AddRange(GenerateClients(50, true).ToList());
            clientes.AddRange(GenerateClients(50, false).ToList());

            return clientes;
        }

        public void Dispose()
        {
        }
    }
}
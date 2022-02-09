using Features.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests._01_Traits
{
     public class ClientTests
    {
        [Fact(DisplayName = "New Valid Client")]
        [Trait("Category", "Client Trait Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = new Client(
                Guid.NewGuid(),
                name: "Luiz Felipe",
                lastName: "Oliveira",
                birthDate: new DateTime(1994,11,29),
                email: "luiz.pepe@hotmail.com",
                active: true,
                registerDate: DateTime.Now);

            // Act
            var result = client.IsValid();

            // Assert
            Assert.True(result);
            Assert.Empty(client.ValidationResult.Errors);
        }

        [Fact(DisplayName = "New Invalid Client")]
        [Trait("Category", "Client Trait Tests")]
        public void Client_NewClient_MustBeInValid()
        {
            // Arrange
            var client = new Client(
                Guid.NewGuid(),
                name: "",
                lastName: "",
                birthDate: DateTime.Now,
                email: "luiz.pepe2hotmail.com",
                active: true,
                registerDate: DateTime.Now);

            // Act
            var result = client.IsValid();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(client.ValidationResult.Errors);
        }
    }
}

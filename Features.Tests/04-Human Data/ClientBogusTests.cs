using System.Linq;
using Xunit;

namespace Features.Tests._04_Human_Data
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientBogusTests
    {
        private readonly ClientTestsBogusFixture _clientTestsFixture;

        public ClientBogusTests(ClientTestsBogusFixture clientTestsFixture)
        {
            _clientTestsFixture = clientTestsFixture;
        }


        [Fact(DisplayName = "New Valid Client")]
        [Trait("Category", "Client Bogus Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = _clientTestsFixture.GenerateValidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Empty(client.ValidationResult.Errors);
        }

        [Fact(DisplayName = "New Invalid Client")]
        [Trait("Category", "Client Bogus Tests")]
        public void Client_NewClient_MustBeInvalid()
        {
            // Arrange
            var client = _clientTestsFixture.GenerateInvalidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.False(result);
            Assert.NotEmpty(client.ValidationResult.Errors);
        }
    }
}
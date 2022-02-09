using Xunit;

namespace Features.Tests._02_Fixtures
{
    [Collection(nameof(ClientCollection))]
    public class InvalidTestClient
    {
        private readonly ClientTestsFixture _client;

        public InvalidTestClient(ClientTestsFixture client)
        {
            this._client = client;
        }

        [Fact(DisplayName = "New Invalid Client")]
        [Trait("Category", "Client Fixture Tests")]
        public void Client_NewClient_MustBeInValid()
        {
            // Arrange
            var client = this._client.GenerateInValidClient();

            // Act
            var result = client.IsValid();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(client.ValidationResult.Errors);
        }
    }
}

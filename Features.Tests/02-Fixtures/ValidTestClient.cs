using Xunit;

namespace Features.Tests._02_Fixtures
{
    [Collection(nameof(ClientCollection))]
    public class ValidTestClient
    {
        private readonly ClientTestsFixture _client;

        public ValidTestClient(ClientTestsFixture client)
        {
            this._client = client;
        }

        [Fact(DisplayName = "New Valid Client")]
        [Trait("Category", "Client Fixture Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = this._client.GenerateValidClient();

            // Act
            var result = client.IsValid();

            // Assert
            Assert.True(result);
            Assert.Empty(client.ValidationResult.Errors);
        }
    }
}
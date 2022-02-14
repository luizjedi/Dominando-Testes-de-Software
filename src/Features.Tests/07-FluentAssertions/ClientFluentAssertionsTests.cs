using Features.Tests._06_AutoMock;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Features.Tests._07_FluentAssertions
{
    [Collection(nameof(ClientAutoMockerCollection))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsFixture;
        private readonly ITestOutputHelper _outputHelper;

        public ClientFluentAssertionsTests(ClientTestsAutoMockerFixture clientTestsFixture, ITestOutputHelper outputHelper)
        {
            this._clientTestsFixture = clientTestsFixture;
            this._outputHelper = outputHelper;
        }


        [Fact(DisplayName = "New Valid Client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = this._clientTestsFixture.GenerateValidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            //Assert.True(result);
            //Assert.Empty(client.ValidationResult.Errors);

            // Assert com FluentAssertions
            result.Should().BeTrue();
            client.ValidationResult.Errors.Should().BeEmpty("Must have none validations errors");
            // Customiza a mensagem de saída do teste
            this._outputHelper.WriteLine($"Were found {client.ValidationResult.Errors.Count} errors in this validation");
        }

        [Fact(DisplayName = "New Invalid Client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeInvalid()
        {
            // Arrange
            var client = this._clientTestsFixture.GenerateInvalidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            //Assert.False(result);
            //Assert.NotEmpty(client.ValidationResult.Errors);

            // Assert com FluentAssertions
            result.Should().BeFalse();
            client.ValidationResult.Errors.Should().NotBeEmpty("Must have validation errors");
            // Customiza a mensagem de saída do teste
            this._outputHelper.WriteLine($"Were found {client.ValidationResult.Errors.Count} errors in this validation");
        }
    }
}
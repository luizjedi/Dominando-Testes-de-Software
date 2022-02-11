using Features.Tests._06_AutoMock;
using FluentAssertions;
using Xunit;

namespace Features.Tests._07_FluentAssertions
{
    [Collection(nameof(ClientAutoMockerCollection))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsFixture;

        public ClientFluentAssertionsTests(ClientTestsAutoMockerFixture clientTestsFixture)
        {
            _clientTestsFixture = clientTestsFixture;
        }


        [Fact(DisplayName = "New Valid Client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = _clientTestsFixture.GenerateValidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            //Assert.True(result);
            //Assert.Empty(client.ValidationResult.Errors);

            // Assert com FluentAssertions
            result.Should().BeTrue();
            client.ValidationResult.Errors.Should().BeEmpty("Must have none validations errors");
        }

        [Fact(DisplayName = "New Invalid Client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeInvalid()
        {
            // Arrange
            var client = _clientTestsFixture.GenerateInvalidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            //Assert.False(result);
            //Assert.NotEmpty(client.ValidationResult.Errors);

            // Assert com FluentAssertions
            result.Should().BeFalse();
            client.ValidationResult.Errors.Should().NotBeEmpty("Must have validation errors");
        }
    }
}
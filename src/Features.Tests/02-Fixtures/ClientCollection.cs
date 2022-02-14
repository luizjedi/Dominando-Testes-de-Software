using Xunit;

namespace Features.Tests._02_Fixtures
{
    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<ClientTestsFixture>
    { }
}

using Xunit;

namespace Features.Tests._06_AutoMock
{
    [CollectionDefinition(nameof(ClientAutoMockerCollection))]
    public class ClientAutoMockerCollection : ICollectionFixture<ClientTestsAutoMockerFixture>
    {
    }
}

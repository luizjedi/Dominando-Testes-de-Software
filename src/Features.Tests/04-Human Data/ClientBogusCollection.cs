using Xunit;

namespace Features.Tests._04_Human_Data
{
    [CollectionDefinition(nameof(ClientBogusCollection))]
    public class ClientBogusCollection : ICollectionFixture<ClientTestsBogusFixture>
    { }
}
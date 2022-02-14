using Xunit;

namespace Features.Tests._08_Skip
{
    public class TestFailsForSomeReason
    {
        [Fact(DisplayName = "New Client 2.0", Skip = "New Version 2.0 Breaking")]
        [Trait("Category", "Escapando dos Testes")]
        public void Test_NotPassing_NewVersionNotCompatible()
        {
            // Assert
            Assert.True(false);
        }
    }
}

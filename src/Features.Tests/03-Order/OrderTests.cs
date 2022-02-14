using Xunit;

namespace Features.Tests._03_Order
{
    [TestCaseOrderer("Features.Tests._03_Order.PriorityOrderer", "Features.Tests")]
    public class OrderTests
    {
        public static bool CallTest1;
        public static bool CallTest2;
        public static bool CallTest3;
        public static bool CallTest4;

        [Fact(DisplayName = "Test 04"), TestPriority(3)]
        [Trait("Category", "Order Tests")]
        public void Test04()
        {
            CallTest4 = true;

            Assert.True(CallTest3);
            Assert.True(CallTest1);
            Assert.False(CallTest2);
        }

        [Fact(DisplayName = "Test 01"), TestPriority(2)]
        [Trait("Category", "Order Tests")]
        public void Test01()
        {
            CallTest1 = true;

            Assert.True(CallTest3);
            Assert.False(CallTest4);
            Assert.False(CallTest2);
        }

        [Fact(DisplayName = "Test 03"), TestPriority(1)]
        [Trait("Category", "Order Tests")]
        public void Test03()
        {
            CallTest3 = true;

            Assert.False(CallTest1);
            Assert.False(CallTest2);
            Assert.False(CallTest4);
        }

        [Fact(DisplayName = "Test 02"), TestPriority(4)]
        [Trait("Category", "Order Tests")]
        public void Test02()
        {
            CallTest2 = true;

            Assert.True(CallTest3);
            Assert.True(CallTest4);
            Assert.True(CallTest1);
        }
    }
}
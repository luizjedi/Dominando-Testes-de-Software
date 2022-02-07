using Xunit;

namespace Demo.TesteDeUnidade
{
    public class CalculatorTestes
    {
        [Fact]
        public void Calculator_Sum_ReturnSumValue()
        {
            // Arrange
            var calculator = new Calculator();

            // Act 
            var result = calculator.Sum(2, 2.5);

            // Assert
            Assert.Equal(expected: 4.5, actual: result);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,2,4)]
        [InlineData(4,51,55)]
        [InlineData(1,2,3)]
        [InlineData(1,4,5)]
        [InlineData(1,5,6)]
        public void Calculator_Sum_ReturnCorrectSumValue(double v1, double v2, double total)
        {
            // Arrange
            var calculator = new Calculator();

            // Act 
            var result = calculator.Sum(v1, v2);

            // Assert
            Assert.Equal(expected: total, actual: result);
        }

        [Fact]
        public void Calculator_Split_ReturnSplitValue()
        {
            // Arrange
            var calculator = new Calculator();

            // Act 
            var result = calculator.Split(6, 2);

            // Assert
            Assert.Equal(expected: 3, actual: result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(8, 2, 4)]
        [InlineData(25, 5, 5)]
        [InlineData(9, 3, 3)]
        [InlineData(20, 4, 5)]
        [InlineData(30, 5, 6)]
        public void Calculator_Split_ReturnCorrectSplitValue(int v1, int v2, int total)
        {
            // Arrange
            var calculator = new Calculator();

            // Act 
            var result = calculator.Split(v1, v2);

            // Assert
            Assert.Equal(expected: total, actual: result);
        }
    }
}

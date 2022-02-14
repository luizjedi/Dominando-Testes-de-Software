using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertNumbersTests
    {
        [Fact]
        public void Calculator_Sum_MustBeEqual()
        {
            // Arrange 
            var calc = new Calculator();

            // Act 
            var result = calc.Sum(1, 2);

            // Assert
            Assert.Equal(expected: 3, actual: result);
        }

        [Fact]
        public void Calculator_Sum_NotMustBeEqual()
        {
            // Arrange 
            var calc = new Calculator();

            // Act 
            var result = calc.Sum(1.13123123123, 2.2312313123);

            // Assert
            Assert.NotEqual(expected: 3.3, actual: result, precision: 1);
        }
    }
}

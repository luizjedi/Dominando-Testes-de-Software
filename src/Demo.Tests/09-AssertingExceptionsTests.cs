using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertingExceptionsTests
    {
        [Fact]
        public void Calculator_Split_DivisionByZeroMShouldReturnError()
        {
            // Arrange 
            var calc = new Calculator();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(testCode: () => calc.Split(10, 0));
        }

        [Fact]
        public void Employee_Salary_SalaryBelowPermissionShouldReturnError()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<Exception>(testCode: () => EmployeeFactory.Create("Luiz", salary: 250));

            Assert.Equal(expected: "Salary below allowed!", actual: exception.Message);

        }
    }
}

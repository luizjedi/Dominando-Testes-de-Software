using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertingRangesTests
    {
        [Theory]
        [InlineData(700)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(15000)]
        public void Employee_Salary_SalaryRangesMustBeFollowProfessionalLevels(double salary)
        {
            // Arrange & Act
            var employee = new Employee("Luiz", salary);

            // Assert
            if (employee.ProfessionalLevel == Enums.ProfessionalLevel.Junior)
                Assert.InRange(actual: employee.Salary, low: 500, high: 1999);

            if (employee.ProfessionalLevel == Enums.ProfessionalLevel.Full)
                Assert.InRange(actual: employee.Salary, low: 2000, high: 7999);

            if (employee.ProfessionalLevel == Enums.ProfessionalLevel.Senior)
                Assert.InRange(actual: employee.Salary, low: 8000, high: double.MaxValue);

            Assert.NotInRange(actual: employee.Salary, low: 0, high: 499);
        }
    }
}

using Demo.Domain;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertingObjectTypesTests
    {
        [Fact]
        public void EmployeeFactory_Create_ReturnEmployeeType()
        {
            // Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", salary: 10000);

            // Assert
            Assert.IsType<Employee>(employee); 
        }

        [Fact]
        public void EmployeeFactory_Create_MustBeReturnDerivativePersonType()
        {
            // Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", salary: 15000);

            // Assert
            Assert.IsAssignableFrom<Person>(employee); 
        }

    }
}

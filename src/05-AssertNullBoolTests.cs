using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Employee_Name_NotNullOrEmpty()
        {
            // Arrange & Act
            var employee = new Employee(name:"", salary: 1000);

            // Assert 
            Assert.False(string.IsNullOrEmpty(employee.Name));
        }

        [Fact]
        public void Employee_Nickname_MustBeNull()
        {
            // Arrange & Act
            var employee = new Employee(name: "Luiz", salary: 1000);

            // Assert 
            Assert.Null(employee.Nickname);

            // Assert Bool
            Assert.True(string.IsNullOrEmpty(employee.Nickname));
            Assert.False(condition: employee.Nickname?.Length > 0);
        }
    }
}

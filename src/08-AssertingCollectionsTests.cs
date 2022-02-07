using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertingCollectionsTests
    {
        [Fact]
        public void Employee_Skills_NotHaveEmptySkills()
        {
            // Arrange & Act
            var employee = EmployeeFactory.Create("Luiz", salary: 10000);

            // Assert
            Assert.All(employee.Skills, action: skill => Assert.False(string.IsNullOrWhiteSpace(skill)));
        }

        [Fact]
        public void Employee_Skills_JuniorMustHaveBasicSkill()
        {
            // Arrange & Act 
            var employee = EmployeeFactory.Create("Luiz", salary: 15000);

            // Assert
            // Contains consegue comparar apenas 1 por vez item da lista 
            Assert.Contains(expected: "OOP", employee.Skills);
            Assert.Contains(expected: "Programming Logic", employee.Skills);
        }

        [Fact]
        public void Employee_Skills_JuniorNotHaveAdvancedSkill()
        {
            // Arrange & Act 
            var employee = EmployeeFactory.Create("Luiz", salary: 15000);

            var advancedSkills = new List<string>
            {
                "Tests",
                "MIcroservice"
            };

            // Assert
            Assert.NotEqual(expected: advancedSkills, employee.Skills);
        }

        [Fact]
        public void Employee_Skills_FullMustHaveTestSkill()
        {
            // Arrange & Act 
            var employee = EmployeeFactory.Create("Luiz", salary: 15000);

            // Assert
            Assert.Contains(expected: "Programming Logic", employee.Skills);
            Assert.Contains(expected: "OOP", employee.Skills);
            Assert.Contains(expected: "Tests", employee.Skills);
        }

        [Fact]
        public void Employee_Skills_FullNotHaveLastAdvancedSkill()
        {
            // Arrange & Act 
            var employee = EmployeeFactory.Create("Luiz", salary: 15000);

            var fullSkills = new List<string>()
            {
                "Programming Logic",
                "OOP",
                "Tests"
            };

            // Assert
            Assert.NotEqual(expected: fullSkills, employee.Skills);
        }

        [Fact]
        public void Employee_Skills_SeniorMustHaveAllSkill()
        {
            // Arrange & Act 
            var employee = EmployeeFactory.Create("Luiz", salary: 15000);

            var skills = new List<string>()
            {
                "Programming Logic",
                "OOP",
                "Tests",
                "MicroServices"
            };

            // Assert
            Assert.Equal(expected: skills, employee.Skills);
        }
    }
}


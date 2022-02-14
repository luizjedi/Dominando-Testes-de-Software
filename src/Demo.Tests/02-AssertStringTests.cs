using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.TesteDeUnidade
{
    public class AssertStringTests
    {
        [Fact]
        public void StringTools_JoinNames_ReturnFullName()
        {
            // Arrange 
            var sTools = new StringTools();

            // Act 
            var fullName = sTools.Join("Luiz Felipe", "Oliveira");

            // Assert
            Assert.Equal(expected: "Luiz Felipe Oliveira", actual: fullName);
        }

        [Fact]
        public void StringTools_JoinNames_CaseIgnore()
        {
            // Arrange 
            var sTools = new StringTools();

            // Act 
            var fullName = sTools.Join("Luiz Felipe", "Oliveira");

            // Assert
            Assert.Equal(expected: "Luiz Felipe Oliveira", actual: fullName, ignoreCase: true);
        }

        [Fact]
        public void StringTools_JoinNames_ContainsTextPart()
        {
            // Arrange 
            var sTools = new StringTools();

            // Act 
            var fullName = sTools.Join("Luiz Felipe", "Oliveira");

            // Assert
            Assert.Contains(expectedSubstring: "elipe", actualString: fullName);
        }

        [Fact]
        public void StringTools_JoinNames_StartsWith()
        {
            // Arrange 
            var sTools = new StringTools();

            // Act 
            var fullName = sTools.Join("Luiz Felipe", "Oliveira");

            // Assert
            Assert.StartsWith(expectedStartString: "Lui", actualString: fullName);
        }

        [Fact]
        public void StringTools_JoinNames_EndsWith()
        {
            // Arrange 
            var sTools = new StringTools();

            // Act 
            var fullName = sTools.Join("Luiz Felipe", "Oliveira");

            // Assert
            Assert.EndsWith(expectedEndString: "ira", actualString: fullName);
        }

        [Fact]
        public void StringTools_JoinNames_ValidateRegularExpression()
        {
            // Arrange 
            var sTools = new StringTools();

            // Act 
            var fullName = sTools.Join("Luiz Felipe", "Oliveira");

            // Assert
            Assert.Matches(expectedRegexPattern: "[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", actualString: fullName);
        }
    }
}

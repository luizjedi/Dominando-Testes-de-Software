﻿using Features.Core;
using FluentAssertions;
using Xunit;

namespace Features.Tests._09_Code_Coverage
{
    public class CpfValidationTests
    {
        [Theory(DisplayName = "Valid CPF")]
        [Trait("Category", "CPF Validation Tests")]
        [InlineData("15231766607")]
        [InlineData("78246847333")]
        [InlineData("64184957307")]
        [InlineData("21681764423")]
        [InlineData("13830803800")]
        public void Cpf_ValidateMultipleNumbers_AllMustBeValid(string cpf)
        {
            // Assert
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.IsValid(cpf).Should().BeTrue();
        }

        [Theory(DisplayName = "Invalid CPF")]
        [Trait("Category", "CPF Validation Tests")]
        [InlineData("15231766607213")]
        [InlineData("528781682082")]
        [InlineData("35555868512")]
        [InlineData("36014132822")]
        [InlineData("72186126500")]
        [InlineData("23775274811")]
        public void Cpf_ValidateMultipleNumbers_AllMustBeInvalid(string cpf)
        {
            // Assert
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.IsValid(cpf).Should().BeFalse();
        }
    }
}
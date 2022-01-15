using BLL.Utilities;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Utilities
{
    public class ArgumentsValidatorTests
    {
        [Fact]
        public void ValidateNArguments_ArgumentsArrayOfLengthN_N_CommandName_ArgumentsArray()
        {
            // Arrange
            var argumentsValidator = new ArgumentsValidator();
            int n = 2;
            string[] arguments = { "a", "b" };
            string commandName = "test";

            // Act
            var res = argumentsValidator.ValidateNArguments(arguments, n, commandName);

            // Assert
            res.Should().Equal(arguments);
        }

        [Fact]
        public void ValidateNArguments_ArgumentsArrayOfNotNLength_N_CommandName_ArgumentsArray()
        {
            // Arrange
            var argumentsValidator = new ArgumentsValidator();
            int n = 2;
            string[] arguments = { "a" };
            string commandName = "test";

            // Act
            Action act = () => argumentsValidator.ValidateNArguments(arguments, n, commandName);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

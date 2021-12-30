using BLL.Utils;
using Core.Dataclasses;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Utils
{
    public class ArgumentsValidatorTests
    {
        [Fact]
        public void ValidateNArguments_ArgumentsArrayOfLengthN_N_CommandName_ArgumentsArray()
        {
            // Arrange
            var argumentsValidator = new ArgumentsValidator();
            int n = 2;
            string[] args = { "a", "b" };
            string cmdName = "test";

            // Act
            var res = argumentsValidator.ValidateNArguments(args, n, cmdName);

            // Assert
            res.Should().Equal(args);
        }
        [Fact]
        public void ValidateNArguments_ArgumentsArrayOfNotNLength_N_CommandName_ArgumentsArray()
        {
            // Arrange
            var argumentsValidator = new ArgumentsValidator();
            int n = 2;
            string[] args = { "a" };
            string cmdName = "test";

            // Act
            Action act = () => argumentsValidator.ValidateNArguments(args, n, cmdName);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }

    }
}

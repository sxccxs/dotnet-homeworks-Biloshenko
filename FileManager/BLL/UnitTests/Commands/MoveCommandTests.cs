using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class MoveCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_SetCurrentDirectoryToBeTheGivenOne()
        {
            // Arrange
            string folderName = "MoveCommandTests",
                   folderPath = Path.Combine("/", folderName);
            string[] arguments = { folderPath };
            var command = new MoveCommand();
            var folderInfo = Directory.CreateDirectory(folderPath);

            // Act
            command.Execute(arguments);

            // Assert
            folderInfo.FullName.Should().Be(Directory.GetCurrentDirectory());

            // Clean
            Directory.SetCurrentDirectory("/");
            Directory.Delete(folderPath);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new MoveCommand();
            string[] arguments = Array.Empty<string>();

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }

        [Fact]
        public void Execute_MoreThenOneArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new MoveCommand();
            string[] arguments = { "a", "b" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

using Xunit;
using FluentAssertions;
using BLL.Commands;
using Core.Exceptions;

namespace BLL.UnitTests.Commands
{
    public class MoveCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_SetCurrentDirectoryToBeTheFivenOne()
        {
            // Arrange
            string folderName = "MoveCommandTests",
                   folderPath = Path.Combine("/", folderName);
            string[] args = { folderPath };
            var cmd = new MoveCommand();
            var folderInfo = Directory.CreateDirectory(folderPath);

            // Act
            cmd.Execute(args);

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
            var cmd = new MoveCommand();
            string[] args = Array.Empty<string>();

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
        [Fact]
        public void Execute_MoreThenOneArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new MoveCommand();
            string[] args = { "a", "b" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class MakeDirectoryCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_CreateNewDirectory()
        {
            // Arrange
            var command = new MakeDirectoryCommand();
            string baseFolder = "/",
                   folderName = "MakeDirectoryCommandTests",
                   folderPath = Path.Combine(baseFolder, folderName);
            string[] arguments = { folderPath };

            // Act
            command.Execute(arguments);

            // Assert
            Directory.Exists(folderPath).Should().BeTrue();

            // Clean
            Directory.Delete(folderPath);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new MakeDirectoryCommand();
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
            var command = new MakeDirectoryCommand();
            string[] arguments = { "a", "b" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class DeleteDirectoryCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_RemoveDirectory()
        {
            // Arrange
            string folderName = "DeleteDirectoryCommandTests",
                   basePath = "/",
                   path = Path.Combine(basePath, folderName);
            string[] arguments = { path };
            Directory.CreateDirectory(path);
            var command = new DeleteDirectoryCommand();

            // Act
            command.Execute(arguments);

            // Assert
            Directory.Exists(path).Should().BeFalse();
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new DeleteDirectoryCommand();
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
            var command = new DeleteDirectoryCommand();
            string[] arguments = { "a", "b" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class RenameDirectoryCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_NewName_RenameDirectory()
        {
            // Arrange
            var command = new RenameDirectoryCommand();
            string baseFolder = "/",
                   folderName = "RenameDirectoryCommandTests",
                   newFolderName = "RenameDirectoryCommandTestsNew",
                   folderPath = Path.Combine(baseFolder, folderName),
                   newFolderPath = Path.Combine(baseFolder, newFolderName);
            string[] arguments = { folderPath, newFolderPath };
            Directory.CreateDirectory(folderPath);

            // Act
            command.Execute(arguments);

            // Assert
            Directory.Exists(folderPath).Should().BeFalse();
            Directory.Exists(newFolderPath).Should().BeTrue();

            // Clean
            Directory.Delete(newFolderPath);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new RenameDirectoryCommand();
            string[] arguments = Array.Empty<string>();

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }

        [Fact]
        public void Execute_OneArgument_InvalidArgumentException()
        {
            // Arrange
            var command = new RenameDirectoryCommand();
            string[] arguments = { "a" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }

        [Fact]
        public void Execute_MoreThenTwoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new RenameDirectoryCommand();
            string[] arguments = { "a", "b", "c" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

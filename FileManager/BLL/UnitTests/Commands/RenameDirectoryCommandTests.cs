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
            var cmd = new RenameDirectoryCommand();
            string baseFolder = "/",
                   folderName = "RenameDirectoryCommandTests",
                   newFolderName = "RenameDirectoryCommandTestsNew",
                   folderPath = Path.Combine(baseFolder, folderName),
                   newFolderPath = Path.Combine(baseFolder, newFolderName);
            string[] args = { folderPath, newFolderPath };
            Directory.CreateDirectory(folderPath);

            // Act
            cmd.Execute(args);

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
            var cmd = new RenameDirectoryCommand();
            string[] args = Array.Empty<string>();

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
        [Fact]
        public void Execute_OneArgument_InvalidArgumentException()
        {
            // Arrange
            var cmd = new RenameDirectoryCommand();
            string[] args = { "a" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
        [Fact]
        public void Execute_MoreThenTwoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new RenameDirectoryCommand();
            string[] args = { "a", "b", "c" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

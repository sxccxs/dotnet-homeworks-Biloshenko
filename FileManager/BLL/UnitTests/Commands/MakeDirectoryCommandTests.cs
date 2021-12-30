using Xunit;
using FluentAssertions;
using BLL.Commands;
using Core.Exceptions;

namespace BLL.UnitTests.Commands
{
    public class MakeDirectoryCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_CreateNewDirectory()
        {
            // Arrange
            var cmd = new MakeDirectoryCommand();
            string baseFolder = "/",
                   folderName = "MakeDirectoryCommandTests",
                   folderPath = Path.Combine(baseFolder, folderName);
            string[] args = { folderPath };

            // Act
            cmd.Execute(args);

            // Assert
            Directory.Exists(folderPath).Should().BeTrue();

            // Clean
            Directory.Delete(folderPath);
        }
        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new MakeDirectoryCommand();
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
            var cmd = new MakeDirectoryCommand();
            string[] args = { "a", "b" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

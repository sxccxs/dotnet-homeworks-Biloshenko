using Xunit;
using FluentAssertions;
using BLL.Commands;
using Core.Exceptions;

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
            string[] args = { path };
            Directory.CreateDirectory(path);
            var cmd = new DeleteDirectoryCommand();

            // Act
            cmd.Execute(args);

            // Assert
            Directory.Exists(path).Should().BeFalse();
        }
        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new DeleteDirectoryCommand();
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
            var cmd = new DeleteDirectoryCommand();
            string[] args = { "a", "b" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

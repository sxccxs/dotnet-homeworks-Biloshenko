using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class RenameFileCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_NewFileName_RenameFile()
        {
            // Arrange
            string folderName = "RenameFileCommandTests",
                   fileName = "RenameFileCommandTests",
                   newFileName = "RenameFileCommandTestsNew",
                   folderPath = Path.Combine("/", folderName),
                   filePath = Path.Combine(folderPath, fileName),
                   newFilePath = Path.Combine(folderPath, newFileName);
            string[] arguments = { filePath, newFilePath };
            var command = new RenameFileCommand();
            Directory.CreateDirectory(folderPath);
            File.Create(filePath).Close();

            // Act
            command.Execute(arguments);

            // Assert
            File.Exists(filePath).Should().BeFalse();
            File.Exists(newFilePath).Should().BeTrue();

            // Clean
            Directory.Delete(folderPath, true);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new RenameFileCommand();
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
            var command = new RenameFileCommand();
            string[] arguments = { "a", "b", "c" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

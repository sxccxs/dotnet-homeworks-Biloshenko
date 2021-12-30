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
            string[] args = { filePath, newFilePath };
            var cmd = new RenameFileCommand();
            Directory.CreateDirectory(folderPath);
            File.Create(filePath).Close();

            // Act
            cmd.Execute(args);

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
            var cmd = new RenameFileCommand();
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
            var cmd = new RenameFileCommand();
            string[] args = { "a", "b", "c" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

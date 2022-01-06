using Xunit;
using FluentAssertions;
using BLL.Commands;
using Core.Exceptions;

namespace BLL.UnitTests.Commands
{
    public class DeleteFileCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_RemoveFile()
        {
            // Arrange
            string folderName = "DeleteFileCommandTests",
                   fileName = "DeleteFileCommandTestsFile",
                   folderPath = Path.Combine("/", folderName),
                   filePath = Path.Combine(folderPath, fileName);
            string[] args = { filePath };
            var cmd = new DeleteFileCommand();
            Directory.CreateDirectory(folderPath);
            File.Create(filePath).Close();

            // Act
            cmd.Execute(args);

            // Assert
            File.Exists(filePath).Should().BeFalse();

            // Clean
            Directory.Delete(folderPath);
        }
        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new DeleteFileCommand();
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
            var cmd = new DeleteFileCommand();
            string[] args = { "a", "b" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

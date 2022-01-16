using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

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
            string[] arguments = { filePath };
            var command = new DeleteFileCommand();
            Directory.CreateDirectory(folderPath);
            File.Create(filePath).Close();

            // Act
            command.Execute(arguments);

            // Assert
            File.Exists(filePath).Should().BeFalse();

            // Clean
            Directory.Delete(folderPath);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new DeleteFileCommand();
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
            var command = new DeleteFileCommand();
            string[] arguments = { "a", "b" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

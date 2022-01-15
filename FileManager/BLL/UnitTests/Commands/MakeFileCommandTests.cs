using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class MakeFileCommandTests
    {
        [Fact]
        public void Execute_ValidFilePath_CreateNewFile()
        {
            // Arrange
            string folderName = "MakeFileCommandTests",
                   fileName = "MakeFileCommandTests",
                   folderPath = Path.Combine("/", folderName),
                   filePath = Path.Combine(folderPath, fileName);
            string[] arguments = { filePath };
            var command = new MakeFileCommand();
            Directory.CreateDirectory(folderPath);

            // Act
            command.Execute(arguments);

            // Assert
            File.Exists(filePath).Should().BeTrue();

            // Clean
            Directory.Delete(folderPath, true);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new MakeFileCommand();
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
            var command = new MakeFileCommand();
            string[] arguments = { "a", "b" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

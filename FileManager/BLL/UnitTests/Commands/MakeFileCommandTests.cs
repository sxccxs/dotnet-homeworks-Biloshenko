using Xunit;
using FluentAssertions;
using BLL.Commands;
using Core.Exceptions;

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
            string[] args = { filePath };
            var cmd = new MakeFileCommand();
            Directory.CreateDirectory(folderPath);

            // Act
            cmd.Execute(args);

            // Assert
            File.Exists(filePath).Should().BeTrue();

            // Clean
            Directory.Delete(folderPath, true);
        }
        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new MakeFileCommand();
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
            var cmd = new MakeFileCommand();
            string[] args = { "a", "b" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

using System.Text;
using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class ViewFileCommandTests
    {
        [Fact]
        public void Execute_ValidFilePath_FirstNSymbolsOfFile()
        {
            // Arrange
            var command = new ViewFileCommand();
            string folderName = "ViewFileCommandTests",
                   fileName = "ViewFileCommandTests",
                   folderPath = Path.Combine("/", folderName),
                   filePath = Path.Combine(folderPath, fileName);

            var data = new byte[250];
            new Random().NextBytes(data);
            var resText = Encoding.UTF8.GetString(data)[..200];
            string[] arguments = { filePath };
            Directory.CreateDirectory(folderPath);
            using (var f = File.OpenWrite(filePath))
            {
                f.Write(data);
            }

            // Act
            var res = command.Execute(arguments);

            // Assert
            res.Should().Be(resText);

            // Clean
            Directory.Delete(folderPath, true);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new ViewFileCommand();
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
            var command = new ViewFileCommand();
            string[] arguments = { "a", "b" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

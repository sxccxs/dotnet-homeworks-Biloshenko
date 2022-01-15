using System.Text;
using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class SearchInFileCommandTests
    {
        [Fact]
        public void Execute_ValidDirectoryPath_SearchString_LineWhereTextWasFound()
        {
            // Arrange
            string folderName = "SearchInFileCommandTests",
                   fileName = "SearchInFileCommandTests",
                   queryString = "test",
                   folderPath = Path.Combine("/", folderName),
                   filePath = Path.Combine(folderPath, fileName),
                   resultText = $"Found \"{queryString}\" in file {filePath} at line {1}:\n" +
                                $"    {queryString}\n";
            string[] arguments = { filePath, queryString };
            var command = new SearchInFileCommand();
            Directory.CreateDirectory(folderPath);
            using (var f = File.OpenWrite(filePath))
            {
                f.Write(Encoding.UTF8.GetBytes(queryString));
            }

            // Act
            var res = command.Execute(arguments);

            // Assert
            res.Should().Be(resultText);

            // Clean
            Directory.Delete(folderPath, true);
        }

        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var command = new SearchInFileCommand();
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
            var command = new SearchInFileCommand();
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
            var command = new SearchInFileCommand();
            string[] arguments = { "a", "b", "c" };

            // Act
            Action act = () => command.Execute(arguments);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }
    }
}

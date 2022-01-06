using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using System.Text;
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
            string[] args = { filePath, queryString };
            var cmd = new SearchInFileCommand();
            Directory.CreateDirectory(folderPath);
            using (var f = File.OpenWrite(filePath))
            {
                f.Write(Encoding.UTF8.GetBytes(queryString));
            }

            // Act
            var res = cmd.Execute(args);

            // Assert
            res.Should().Be(resultText);

            // Clean
            Directory.Delete(folderPath, true);
        }
        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new SearchInFileCommand();
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
            var cmd = new SearchInFileCommand();
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
            var cmd = new SearchInFileCommand();
            string[] args = { "a", "b", "c" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }

    }
}

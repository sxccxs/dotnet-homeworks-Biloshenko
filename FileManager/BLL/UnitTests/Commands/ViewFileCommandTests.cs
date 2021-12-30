using BLL.Commands;
using Core.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.UnitTests.Commands
{
    public class ViewFileCommandTests
    {
        [Fact]
        public void Execute_ValidFilePath_FirstNSymbolsOfFile()
        {
            // Arrange
            var cmd = new ViewFileCommand();
            string folderName = "ViewFileCommandTests",
                   fileName = "ViewFileCommandTests",
                   folderPath = Path.Combine("/", folderName),
                   filePath = Path.Combine(folderPath, fileName);

            var data = new byte[250];
            new Random().NextBytes(data);
            var resText = Encoding.UTF8.GetString(data)[..200];
            string[] args = { filePath };
            Directory.CreateDirectory(folderPath);
            using (var f = File.OpenWrite(filePath))
            {
                f.Write(data);
            }

            // Act
            var res = cmd.Execute(args);

            // Assert
            res.Should().Be(resText);

            // Clean
            Directory.Delete(folderPath, true);
        }
        [Fact]
        public void Execute_NoArguments_InvalidArgumentException()
        {
            // Arrange
            var cmd = new ViewFileCommand();
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
            var cmd = new ViewFileCommand();
            string[] args = { "a", "b" };

            // Act
            Action act = () => cmd.Execute(args);

            // Assert
            act.Should().Throw<InvalidArgumentException>();
        }

    }
}

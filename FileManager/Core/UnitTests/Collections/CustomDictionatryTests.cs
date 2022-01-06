using Xunit;
using FluentAssertions;
using Core.Collections;
using Core.Exceptions;

namespace Core.UnitTests.Collections
{
    public class CustomDictionatryTests
    {
        [Fact]
        public void Add_IntKey_StringValue_AddKeyValuePairToDict()
        {
            // Arrange
            var dict = new CustomDictionary<int, string>();

            // Act
            dict.Add(1, "test");

            // Assert
            dict.Get(1).Should().Be("test");
        }

        [Fact]
        public void Add_NullKey_IntValue_ArgumentNullException()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();

            // Act
            Action act = () => dict.Add(null, 1);

            // Assert
            act.Should().Throw<ArgumentNullException>();

        }
        [Fact]
        public void Add_StringKey_IntValue_DublicateKeyException()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();

            // Act
            dict.Add("a", 1);
            Action act = () => dict.Add("a", 2);

            // Assert
            act.Should().Throw<DublicateKeyException>();

        }
        [Fact]
        public void Get_ValidKey_ValueAtGivenKey()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();
            dict.Add("a", 2);

            // Act
            var key = dict.Get("a");

            // Assert
            key.Should().Be(2);
        }
        [Fact]
        public void Get_NotExistingKey_KeyNotFoundException()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();

            // Act
            Action act = () => dict.Get("a");

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
        [Fact]
        public void Get_NullKey_KeyNotFoundException()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();

            // Act
            Action act = () => dict.Get(null);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
        [Fact]
        public void Contains_ExistingKey_True()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();
            dict.Add("a", 1);

            // Act
            bool check = dict.Contains("a");

            // Assert
            check.Should().BeTrue();
        }
        [Fact]
        public void Contains_NotExistingKey_False()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();
            dict.Add("b", 1);

            // Act
            bool check = dict.Contains("a");

            // Assert
            check.Should().BeFalse();
        }
        [Fact]
        public void Remove_NotExistingKey_KeyNotFoundException()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();
            dict.Add("b", 1);

            // Act
            Action act = () => dict.Remove("a");

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
        [Fact]
        public void Remove_ExistingKey_RemoveKeyValuePairWithGivenKey()
        {
            // Arrange
            var dict = new CustomDictionary<string, int>();
            dict.Add("b", 1);

            // Act
            dict.Remove("b");
            Action act = () => dict.Get("b");

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}

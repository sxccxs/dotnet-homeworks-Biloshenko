using Xunit;
using FluentAssertions;
using Core.Collections;

namespace Core.UnitTests.Collections
{
    public class CustomLinkedListTests
    {
        [Fact]
        public void Add_Int_AppenIntToList()
        {

            // Arrange
            CustomLinkedList<int> list = new();

            // Act
            list.Add(1);

            // Assert
            list[0].Should().Be(1);
        }

        [Fact]
        public void At_ValidIndex_ValueAtGivenIndex()
        {
            // Arrange
            CustomLinkedList<int> list = new();
            list.Add(1);


            // Act
            var res = list.At(0);

            // Assert
            res.Should().Be(1);
        }

        [Fact]
        public void At_InvalidIndex_IndexOutOfRangeException()
        {
            // Arrange
            CustomLinkedList<int> list = new();


            // Act
            Action act = () => list.At(0);

            // Assert
            act.Should().Throw<IndexOutOfRangeException>();

        }
        [Fact]
        public void Set_ValidIndex_ValidValue_SetValueAtGivenIndexToBeEqualToGivenValue()
        {
            // Arrange
            CustomLinkedList<int> list = new();
            list.Add(1);

            // Act
            list.Set(0, 2);

            // Assert
            list[0].Should().Be(2);

        }
        [Fact]
        public void Set_InvalidIndex_ValidValue_IndexOutOfRangeException()
        {
            // Arrange
            CustomLinkedList<int> list = new();

            // Act
            Action act = () => list.Set(0, 2);

            // Assert
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Pop_CallFromNonEmptyList_RemoveLastElementAndReturnIt()
        {
            // Arrange
            CustomLinkedList<int> list = new();
            list.Add(1);

            // Act
            var res = list.Pop();

            // Assert
            res.Should().Be(1);
            list.Count.Should().Be(0);
        }
        [Fact]
        public void Pop_CallFromEmptyList_InvalidOperationException()
        {
            // Arrange
            CustomLinkedList<int> list = new();

            // Act
            Action act = () => list.Pop();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void Remove_ExistingIndex_RemoveElementWithgivenIndexFromArray()
        {
            // Arrange
            CustomLinkedList<int> list = new();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // Act
            list.Remove(1);

            // Assert
            list[0].Should().Be(1);
            list[1].Should().Be(3);
            list.Count.Should().Be(2);
        }
        [Fact]
        public void Remove_FirstExistingIndex_RemoveFirstElementFromArray()
        {
            // Arrange
            CustomLinkedList<int> list = new();
            list.Add(1);

            // Act
            list.Remove(0);

            // Assert
            list.Count.Should().Be(0);
        }
        [Fact]
        public void Remove_NotExistingIndex_IndexOutOfRangeException()
        {

            // Arrange
            CustomLinkedList<int> list = new();

            // Act
            Action act = () => list.Remove(1);

            // Assert
            act.Should().Throw<IndexOutOfRangeException>();
        }
        [Fact]
        public void Count_ListWithTwoElements_Two()
        {

            // Arrange
            CustomLinkedList<int> list = new();
            list.Add(1);
            list.Add(2);

            // Act
            var res = list.Count;

            // Assert
            res.Should().Be(2);
        }


    }
}

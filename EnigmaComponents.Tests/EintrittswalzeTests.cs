using EnigmaComponents.Classes;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class EintrittswalzeTests
    {
        public EintrittswalzeTests()
        {
            walze = new Eintrittswalze();
        }

        Eintrittswalze walze;

        [Theory]
        [InlineData('A', 0)]
        [InlineData('C', 2)]
        [InlineData('K', 10)]
        [InlineData('Z', 25)]
        public void GetIndexInputChar_WiringSet_CorrectIndexReturned(char rightChar, int expectedIndex)
        {
            // Arrange

            // Act
            int indexRightChar = walze.GetIndexInputChar(rightChar);

            // Assert
            Assert.Equal(expectedIndex, indexRightChar);
        }

#region Testing all standard wirings

        [Theory]
        [InlineData('A', 'A')]
        [InlineData('B', 'B')]
        [InlineData('C', 'C')]
        [InlineData('D', 'D')]
        [InlineData('E', 'E')]
        [InlineData('F', 'F')]
        [InlineData('G', 'G')]
        [InlineData('H', 'H')]
        [InlineData('I', 'I')]
        [InlineData('J', 'J')]
        [InlineData('K', 'K')]
        [InlineData('L', 'L')]
        [InlineData('M', 'M')]
        [InlineData('N', 'N')]
        [InlineData('O', 'O')]
        [InlineData('P', 'P')]
        [InlineData('Q', 'Q')]
        [InlineData('R', 'R')]
        [InlineData('S', 'S')]
        [InlineData('T', 'T')]
        [InlineData('U', 'U')]
        [InlineData('V', 'V')]
        [InlineData('W', 'W')]
        [InlineData('X', 'X')]
        [InlineData('Y', 'Y')]
        [InlineData('Z', 'Z')]
        public void GetLeftChar_WiringSet_ExpectedWiringSet(char inFromRight, char expectedLeftChar)
        {
            // Arrange

            // Act
            char actualLeftChar = walze.GetLeftChar(inFromRight);

            // Assert
            Assert.Equal(expectedLeftChar, actualLeftChar);
        }

        [Theory]
        [InlineData('A', 'A')]
        [InlineData('B', 'B')]
        [InlineData('C', 'C')]
        [InlineData('D', 'D')]
        [InlineData('E', 'E')]
        [InlineData('F', 'F')]
        [InlineData('G', 'G')]
        [InlineData('H', 'H')]
        [InlineData('I', 'I')]
        [InlineData('J', 'J')]
        [InlineData('K', 'K')]
        [InlineData('L', 'L')]
        [InlineData('M', 'M')]
        [InlineData('N', 'N')]
        [InlineData('O', 'O')]
        [InlineData('P', 'P')]
        [InlineData('Q', 'Q')]
        [InlineData('R', 'R')]
        [InlineData('S', 'S')]
        [InlineData('T', 'T')]
        [InlineData('U', 'U')]
        [InlineData('V', 'V')]
        [InlineData('W', 'W')]
        [InlineData('X', 'X')]
        [InlineData('Y', 'Y')]
        [InlineData('Z', 'Z')]
        public void GetRightChar_WiringSet_ExpectedWiringSet(char inFromLeft, char expectedRightChar)
        {
            // Arrange

            // Act
            char actualRightChar = walze.GetRightChar(inFromLeft);

            // Assert
            Assert.Equal(expectedRightChar, actualRightChar);
        }

#endregion

    }
}

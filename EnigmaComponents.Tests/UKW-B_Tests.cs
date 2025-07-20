using Xunit;
using EnigmaComponents.Classes;

namespace EnigmaComponents.Tests
{
    public class UKW_B_Tests
    {
        public UKW_B_Tests()
        {
            _walze = new UKW_B();
        }

        Umkehrwalze _walze;

        [Theory]
        [InlineData('A', 'Y')]
        [InlineData('B', 'R')]
        [InlineData('C', 'U')]
        [InlineData('D', 'H')]
        [InlineData('E', 'Q')]
        [InlineData('F', 'S')]
        [InlineData('G', 'L')]
        [InlineData('H', 'D')]
        [InlineData('I', 'P')]
        [InlineData('J', 'X')]
        [InlineData('K', 'N')]
        [InlineData('L', 'G')]
        [InlineData('M', 'O')]
        [InlineData('N', 'K')]
        [InlineData('O', 'M')]
        [InlineData('P', 'I')]
        [InlineData('Q', 'E')]
        [InlineData('R', 'B')]
        [InlineData('S', 'F')]
        [InlineData('T', 'Z')]
        [InlineData('U', 'C')]
        [InlineData('V', 'W')]
        [InlineData('W', 'V')]
        [InlineData('X', 'J')]
        [InlineData('Y', 'A')]
        [InlineData('Z', 'T')]
        public void ReflectChar_WiringSet_CorrectCharReflected(char input, char ExpectedChar)
        {
            // Arrange

            // Act
            char actualReflectedChar = _walze.ReflectChar(input);

            // Assert
            Assert.Equal(ExpectedChar, actualReflectedChar);

        }

        [Theory]
        [InlineData('A', 0)]
        [InlineData('I', 8)]
        [InlineData('Z', 25)]
        public void GetIndexInputChar(char input, int expectedIndex)
        {
            // Arrange

            // Act
            int actualIndex = _walze.GetIndexInputChar(input);

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }
    }
}

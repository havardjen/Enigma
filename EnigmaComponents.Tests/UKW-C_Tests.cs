
using Xunit;
using EnigmaComponents.Classes;

namespace EnigmaComponents.Tests
{
    public class UKW_C_Tests
    {
        public UKW_C_Tests()
        {
            _walze = new UKW_C();
        }

        Umkehrwalze _walze;

        [Theory]
        [InlineData('A', 'F')]
        [InlineData('B', 'V')]
        [InlineData('C', 'P')]
        [InlineData('D', 'J')]
        [InlineData('E', 'I')]
        [InlineData('F', 'A')]
        [InlineData('G', 'O')]
        [InlineData('H', 'Y')]
        [InlineData('I', 'E')]
        [InlineData('J', 'D')]
        [InlineData('K', 'R')]
        [InlineData('L', 'Z')]
        [InlineData('M', 'X')]
        [InlineData('N', 'W')]
        [InlineData('O', 'G')]
        [InlineData('P', 'C')]
        [InlineData('Q', 'T')]
        [InlineData('R', 'K')]
        [InlineData('S', 'U')]
        [InlineData('T', 'Q')]
        [InlineData('U', 'S')]
        [InlineData('V', 'B')]
        [InlineData('W', 'N')]
        [InlineData('X', 'M')]
        [InlineData('Y', 'H')]
        [InlineData('Z', 'L')]
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

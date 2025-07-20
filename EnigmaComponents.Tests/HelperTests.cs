using EnigmaComponents.Classes;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class HelperTests
    {
        public HelperTests()
        {}

        [Fact]
        public void GetAlphabetArray_NoInput_OneDimensionalArrayReturned()
        {
            // Arrange
            char[] alphabetReference = GetAlphabet();
            char[] alphabetActual;

            // Act
            alphabetActual = Helper.GetAlphabetArray();

            // Assert
            Assert.Equal(alphabetReference.Length, alphabetActual.Length);

            for(int i = 0; i < alphabetReference.Length; i++)
            {
                Assert.Equal(alphabetReference[i], alphabetActual[i]);
            }
        }


        private char[] GetAlphabet()
        {
            char[] alphabet = new char[26] 
            {
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'J',
                'K',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z'
            };

            return alphabet;
        }
    }
}

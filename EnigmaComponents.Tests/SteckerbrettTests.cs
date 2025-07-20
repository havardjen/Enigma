using EnigmaComponents.Interfaces;
using EnigmaComponents.Classes;
using Xunit;
using EnigmaComponents.Exceptions;

namespace EnigmaComponents.Tests
{
    public class SteckerbrettTests
    {
        public SteckerbrettTests()
        {
            brett = new Steckerbrett();
        }

        ISteckerbrett brett;

        [Fact]
        public void Steckerbrett_ClassInstantiated_PluggingExists()
        {
            // Arrange

            // Act
            brett = new Steckerbrett();

            // Assert
            Assert.NotNull(brett.Plugging);
        }

        [Fact]
        public void EncodeCharFromKeyboard_ClassInstantiatedNoPluggingSet_EachCharSwapsToSameChar()
        {
            // Arrange
            char[] alphabet = Helper.GetAlphabetArray();
            char encodedChar;

            // Act & Assert
            foreach(char charToEncode in alphabet)
            {
                encodedChar = brett.EncodeCharFromKeyboard(charToEncode);
                Assert.Equal(charToEncode, encodedChar);
            }

        }

        [Fact]
        public void EncodeCharFromRotors_ClassInstantiatedNoPluggingSet_EachCharSwapsToSameChar()
        {
            // Arrange
            char[] alphabet = Helper.GetAlphabetArray();
            char displayedChar;

            // Act & Assert
            foreach (char charToDisplay in alphabet)
            {
                displayedChar = brett.EncodeCharFromRotors(charToDisplay);
                Assert.Equal(charToDisplay, displayedChar);
            }

        }

        [Theory]
        [InlineData('A', 'Z')]
        [InlineData('M', 'B')]
        public void InsertCable_ConnectingTwoLetters_TwoLettersSwapped(char from, char to)
        {
            // Arrange
            
            // Act
            brett.InsertCable(from, to);

            // Assert
            Assert.Equal(from, brett.EncodeCharFromKeyboard(to));
            Assert.Equal(from, brett.EncodeCharFromRotors(to));
            Assert.Equal(to, brett.EncodeCharFromKeyboard(from));
            Assert.Equal(to, brett.EncodeCharFromRotors(from));
        }

        [Fact]
        public void InsertCable_FromPlugTaken_PlugTakenExceptionReceived()
        {
            // Arrange
            char from = 'F';
            char to = 'X';

            // Act
            brett.InsertCable(from, to);
            var exception = Record.Exception(() => brett.InsertCable(from, 'B'));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PlugTakenException>(exception);
        }

        [Fact]
        public void RemoveCable_ValidPlugging_CharsSwapToSame()
        {
            // Arrange
            char from = 'Q';
            char to = 'C';
            brett.InsertCable(from, to);

            // Act
            brett.RemoveCable(from, to);

            // Assert
            Assert.Equal(from, brett.EncodeCharFromKeyboard(from));
            Assert.Equal(from, brett.EncodeCharFromRotors(from));
            Assert.Equal(to, brett.EncodeCharFromKeyboard(to));
            Assert.Equal(to, brett.EncodeCharFromRotors(to));
        }

        [Theory]
        [InlineData('D','E')]
        [InlineData('J', 'K')]
        [InlineData('M', 'P')]
        public void RemoveCable_InValidPlugging_InvalidPluggingExceptionExistingUntouched(char fromInput, char toInput)
        {
            // Arrange
            char from = 'D';
            char to = 'K';
            brett.InsertCable(from, to);

            // Act
            var exception = Record.Exception(() => brett.RemoveCable(fromInput, toInput));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidPluggingException>(exception);
            Assert.Equal(from, brett.EncodeCharFromKeyboard(to));
            Assert.Equal(from, brett.EncodeCharFromRotors(to));
            Assert.Equal(to, brett.EncodeCharFromKeyboard(from));
            Assert.Equal(to, brett.EncodeCharFromRotors(from));
        }

    }
}

using EnigmaComponents.Classes;
using EnigmaComponents.Enums;
using EnigmaComponents.Exceptions;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorI_Scramble_Tests
    {
        public RotorI_Scramble_Tests()
        {
            rotor = new RotorI(null);
        }

        char scrambledChar;
        Rotor rotor;

#region public void ScrambleFromRight_GrundstellungA01_CorrectScrambling(char inChar, char outChar)
        [Theory]
        [InlineData('A', 'E')]
        [InlineData('B', 'K')]
        [InlineData('C', 'M')]
        [InlineData('D', 'F')]
        [InlineData('E', 'L')]
        [InlineData('F', 'G')]
        [InlineData('G', 'D')]
        [InlineData('H', 'Q')]
        [InlineData('I', 'V')]
        [InlineData('J', 'Z')]
        [InlineData('K', 'N')]
        [InlineData('L', 'T')]
        [InlineData('M', 'O')]
        [InlineData('N', 'W')]
        [InlineData('O', 'Y')]
        [InlineData('P', 'H')]
        [InlineData('Q', 'X')]
        [InlineData('R', 'U')]
        [InlineData('S', 'S')]
        [InlineData('T', 'P')]
        [InlineData('U', 'A')]
        [InlineData('V', 'I')]
        [InlineData('W', 'B')]
        [InlineData('X', 'R')]
        [InlineData('Y', 'C')]
        [InlineData('Z', 'J')]
        public void ScrambleFromRight_GrundstellungA01_CorrectScrambling(char rightChar, char leftChar)
        {
            // Arrange

            // Act
            scrambledChar = rotor.ScrambleFromRight(rightChar);

            // Assert
            Assert.Equal(leftChar, scrambledChar);
        }
#endregion

#region public void ScrambleFromLeft_GrundstellungA01_CorrectScrambling(char inChar, char outChar)
        [Theory]
        [InlineData('A', 'E')]
        [InlineData('B', 'K')]
        [InlineData('C', 'M')]
        [InlineData('D', 'F')]
        [InlineData('E', 'L')]
        [InlineData('F', 'G')]
        [InlineData('G', 'D')]
        [InlineData('H', 'Q')]
        [InlineData('I', 'V')]
        [InlineData('J', 'Z')]
        [InlineData('K', 'N')]
        [InlineData('L', 'T')]
        [InlineData('M', 'O')]
        [InlineData('N', 'W')]
        [InlineData('O', 'Y')]
        [InlineData('P', 'H')]
        [InlineData('Q', 'X')]
        [InlineData('R', 'U')]
        [InlineData('S', 'S')]
        [InlineData('T', 'P')]
        [InlineData('U', 'A')]
        [InlineData('V', 'I')]
        [InlineData('W', 'B')]
        [InlineData('X', 'R')]
        [InlineData('Y', 'C')]
        [InlineData('Z', 'J')]
        public void ScrambleFromLeft_GrundstellungA01_CorrectScrambling(char rightChar, char leftChar)
        {
            // Arrange

            // Act
            scrambledChar = rotor.ScrambleFromLeft(leftChar);

            // Assert
            Assert.Equal(rightChar, scrambledChar);
        }
#endregion

        [Theory]
        [InlineData('A', 'K', 'B')]
        [InlineData('F', 'M', 'B')]
        [InlineData('X', 'C', 'B')]
        [InlineData('Z', 'T', 'C')]
        public void ScrambleFromRight_GSA01RSSet_CorrectScrambling(char rightChar, char leftChar, char ringStellung)
        {
            // Arrange
            rotor.SetRingStellung(ringStellung);
            rotor.SetPosition('A');

            // Act
            scrambledChar = rotor.ScrambleFromRight(rightChar);

            // Assert
            Assert.Equal(leftChar, scrambledChar);
        }

        [Theory]
        [InlineData('A', 'K', 'A', 'B')]
        [InlineData('B', 'F', 'A', 'B')]
        [InlineData('A', 'E', 'B', 'B')]
        [InlineData('A', 'W', 'Y', 'F')]
        [InlineData('F', 'J', 'Y', 'F')]
        [InlineData('Z', 'Z', 'Y', 'F')]
        [InlineData('A', 'N', 'F', 'X')]
        [InlineData('X', 'Y', 'F', 'X')]
        public void ScrambleFromRight_PositionRingStellungSet_CorrectScrambling(char rightChar, char leftChar, char position, char ringStellung)
        {
            // Arrange
            rotor.SetRingStellung(ringStellung);
            rotor.SetPosition(position);

            // Act
            scrambledChar = rotor.ScrambleFromRight(rightChar);

            // Assert
            Assert.Equal(leftChar, scrambledChar);
        }

        [Theory]
        [InlineData('A', 'K', 'B')]
        [InlineData('F', 'B', 'B')]
        [InlineData('X', 'O', 'B')]
        [InlineData('Z', 'S', 'C')]
        public void ScrambleFromLeft_GSA01RSSet_CorrectScrambling(char leftChar, char rightChar, char ringStellung)
        {
            // Arrange
            rotor.SetRingStellung(ringStellung);
            rotor.SetPosition('A');

            // Act
            scrambledChar = rotor.ScrambleFromLeft(leftChar);

            // Assert
            Assert.Equal(rightChar, scrambledChar);
        }

        [Theory]
        [InlineData('A', 'K', 'A', 'B')]
        [InlineData('B', 'V', 'A', 'B')]
        [InlineData('A', 'U', 'B', 'B')]
        [InlineData('A', 'S', 'Y', 'F')]
        [InlineData('F', 'V', 'Y', 'F')]
        [InlineData('Z', 'Z', 'Y', 'F')]
        [InlineData('Y', 'L', 'W', 'Z')]
        public void ScrambleFromLeft_PositionRingStellungSet_CorrectScrambling(char leftChar, char rightChar, char position, char ringStellung)
        {
            // Arrange
            rotor.SetRingStellung(ringStellung);
            rotor.SetPosition(position);

            // Act
            scrambledChar = rotor.ScrambleFromLeft(leftChar);

            // Assert
            Assert.Equal(rightChar, scrambledChar);
        }

        [Fact]
        public void ScramleFromRight_InvalidCharacter_ScramblingExceptionReturned()
        {
            // Arrange
            char characterToScramble = 'Æ';
            ScramblingExceptionCause expectedCause = ScramblingExceptionCause.InvalidCharacterInput;

            // Act
            var exception = Record.Exception(() => rotor.ScrambleFromRight(characterToScramble));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ScramblingException>(exception);
            Assert.Equal(expectedCause, ((ScramblingException)exception).Cause);
            Assert.Equal(characterToScramble.ToString(), ((ScramblingException)exception).InvalidCharacter);
        }

        [Fact]
        public void ScramleFromLeft_InvalidCharacter_ScramblingExceptionReturned()
        {
            // Arrange
            char characterToScramble = 'Æ';
            ScramblingExceptionCause expectedCause = ScramblingExceptionCause.InvalidCharacterInput;

            // Act
            var exception = Record.Exception(() => rotor.ScrambleFromLeft(characterToScramble));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ScramblingException>(exception);
            Assert.Equal(expectedCause, ((ScramblingException)exception).Cause);
            Assert.Equal(characterToScramble.ToString(), ((ScramblingException)exception).InvalidCharacter);
        }
    }
}

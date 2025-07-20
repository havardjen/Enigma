using EnigmaComponents.Classes;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorIII_Scramble_Tests
    {
        public RotorIII_Scramble_Tests()
        {
            rotor = new RotorIII(null);
        }

        char scrambledChar;
        Rotor rotor;

        [Theory]
        [InlineData('R', 'C', 'W', 'Z')]
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
    }
}

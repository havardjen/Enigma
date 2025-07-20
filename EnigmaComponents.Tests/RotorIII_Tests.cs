using EnigmaComponents.Classes;
using Moq;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorIII_Tests
    {
        public RotorIII_Tests()
        {
            rotor = new RotorIII(null);
        }

        char scrambledChar;
        Rotor rotor;
        Mock<RotorIII> mockRotor;

        [Fact]
        public void MaxIndex_CalculatedFromWiringLength_correctMaxIndex()
        {
            // Arrange
            int expectedMaxIndex = rotor.Wiring.GetLength(0) - 1;

            // Act

            // Assert
            Assert.Equal(expectedMaxIndex, rotor.MaxIndex);
        }

        [Fact]
        public void RotorIII_ClassInstantiated_WiringTableCreated()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(rotor.Wiring);
            Assert.Equal(26, rotor.Wiring.GetLength(0));
        }

        [Fact]
        public void RotorIII_ClassInstantiated_WiringContainsOnlyValidCharacters()
        {
            // Arrange
            char[] alphabet = Helper.GetAlphabetArray();
            bool firstCharIsInvalid = true;
            bool secondCharIsInvalid = true;

            // Act
            for (int i = 0; i < rotor.Wiring.GetLength(0); i++)
            {
                firstCharIsInvalid = true;
                secondCharIsInvalid = true;
                char firstChar = rotor.Wiring[i, 0];
                char secondChar = rotor.Wiring[i, 1];

                foreach (char letter in alphabet)
                {
                    if (firstChar == letter)
                        firstCharIsInvalid = false;

                    if (secondChar == letter)
                        secondCharIsInvalid = false;
                }

                // Assert
                Assert.False(firstCharIsInvalid);
                Assert.False(secondCharIsInvalid);
            }
        }

#region public void ScrambleFromRight_GrundstellungA01_CorrectScrambling(char inChar, char outChar)
        [Theory]
        [InlineData('A', 'B')]
        [InlineData('B', 'D')]
        [InlineData('C', 'F')]
        [InlineData('D', 'H')]
        [InlineData('E', 'J')]
        [InlineData('F', 'L')]
        [InlineData('G', 'C')]
        [InlineData('H', 'P')]
        [InlineData('I', 'R')]
        [InlineData('J', 'T')]
        [InlineData('K', 'X')]
        [InlineData('L', 'V')]
        [InlineData('M', 'Z')]
        [InlineData('N', 'N')]
        [InlineData('O', 'Y')]
        [InlineData('P', 'E')]
        [InlineData('Q', 'I')]
        [InlineData('R', 'W')]
        [InlineData('S', 'G')]
        [InlineData('T', 'A')]
        [InlineData('U', 'K')]
        [InlineData('V', 'M')]
        [InlineData('W', 'U')]
        [InlineData('X', 'S')]
        [InlineData('Y', 'Q')]
        [InlineData('Z', 'O')]
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
        [InlineData('A', 'B')]
        [InlineData('B', 'D')]
        [InlineData('C', 'F')]
        [InlineData('D', 'H')]
        [InlineData('E', 'J')]
        [InlineData('F', 'L')]
        [InlineData('G', 'C')]
        [InlineData('H', 'P')]
        [InlineData('I', 'R')]
        [InlineData('J', 'T')]
        [InlineData('K', 'X')]
        [InlineData('L', 'V')]
        [InlineData('M', 'Z')]
        [InlineData('N', 'N')]
        [InlineData('O', 'Y')]
        [InlineData('P', 'E')]
        [InlineData('Q', 'I')]
        [InlineData('R', 'W')]
        [InlineData('S', 'G')]
        [InlineData('T', 'A')]
        [InlineData('U', 'K')]
        [InlineData('V', 'M')]
        [InlineData('W', 'U')]
        [InlineData('X', 'S')]
        [InlineData('Y', 'Q')]
        [InlineData('Z', 'O')]
        public void ScrambleFromLeft_GrundstellungA01_CorrectScrambling(char rightChar, char leftChar)
        {
            // Arrange

            // Act
            scrambledChar = rotor.ScrambleFromLeft(leftChar);

            // Assert
            Assert.Equal(rightChar, scrambledChar);
        }
#endregion

        [Fact]
        public void Notch_PositionSet_CharM_Returned()
        {
            // Arrange
            char expectedNotch = 'D';
            // Act

            // Assert
            Assert.Equal(expectedNotch, rotor.Notch);
        }

        [Fact]
        public void Turnover_PositionSet_CharE_Returned()
        {
            // Arrange
            char expectedTurnover = 'V';
            // Act

            // Assert
            Assert.Equal(expectedTurnover, rotor.Turnover);
        }

        [Theory]
        [InlineData('A', 'P', 'B')]
        [InlineData('F', 'K', 'B')]
        [InlineData('X', 'V', 'B')]
        [InlineData('Z', 'U', 'C')]
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
        [InlineData('A', 'P', 'A', 'B')]
        [InlineData('B', 'C', 'A', 'B')]
        [InlineData('A', 'B', 'B', 'B')]
        [InlineData('A', 'H', 'Y', 'F')]
        [InlineData('Z', 'N', 'Y', 'F')]
        [InlineData('I', 'K', 'Y', 'F')]
        public void ScrambleFromRight_PositionRingStellungSet_CorrectScrambling(char rightChar, 
                                                                                char leftChar, 
                                                                                char position, 
                                                                                char ringStellung)
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
        [InlineData('A', 'N', 'A', 'B')]
        [InlineData('B', 'U', 'A', 'B')]
        [InlineData('A', 'T', 'B', 'B')]
        [InlineData('A', 'Q', 'Y', 'F')]
        [InlineData('F', 'V', 'Y', 'F')]
        [InlineData('Z', 'E', 'Y', 'F')]
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
        public void Step_RotorStepsToTurnoverPosition_TurnoverEventIsFired()
        {
            // Arrange
            rotor = new RotorIII(1);
            int indexTurnover = rotor.GetIndexInputContact(rotor.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexBeforeTurnover = (indexTurnover == 0) ? 25 : indexTurnover - 1;
            char charBeforeTurnover = rotor.Wiring[indexBeforeTurnover, 0];

            rotor.SetPosition(charBeforeTurnover);
            string expectedRotorName = "RotorIII";

            string rotorName = null;
            rotor.RotorTurnoverEvent += delegate (object sender, EventArgs e)
            {
                rotorName = ((RotorTurnoverEventArgs)e).RotorName;
            };

            // Act
            rotor.Step();

            // Assert
            Assert.NotNull(rotorName);
            Assert.Equal(expectedRotorName, rotorName);
        }

        [Theory]
        [InlineData('A')]
        [InlineData('Y')]
        [InlineData('Z')]
        public void HandleRotorTurnoverEvent_RotorTurnoverEventRaised_NotSteppedPropertySet(char currentPosition)
        {
            // Assert
            rotor.SetPosition(currentPosition);

            mockRotor = new Mock<RotorIII>(null);
            mockRotor.Object.RotorTurnoverEvent += rotor.RotorTurnoverEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.RotorTurnoverEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            // Act
            mockRotor.Object.Step();

            // Assert
            Assert.Equal(currentPosition, rotor.Wiring[rotor.Position, 0]);
            Assert.True(rotor.PreviousRotorIsAtTurnoverPosition);
        }
    }
}

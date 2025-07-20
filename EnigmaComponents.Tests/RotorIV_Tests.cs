using EnigmaComponents.Classes;
using Moq;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorIV_Tests
    {
        public RotorIV_Tests()
        {
            rotor = new RotorIV(null);
        }

        char scrambledChar;
        Rotor rotor;
        Mock<RotorIV> mockRotor;

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
        public void RotorIV_ClassInstantiated_WiringTableCreated()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(rotor.Wiring);
            Assert.Equal(26, rotor.Wiring.GetLength(0));
        }

        [Fact]
        public void RotorIV_ClassInstantiated_WiringContainsOnlyValidCharacters()
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
        [InlineData('A', 'E')]
        [InlineData('B', 'S')]
        [InlineData('C', 'O')]
        [InlineData('D', 'V')]
        [InlineData('E', 'P')]
        [InlineData('F', 'Z')]
        [InlineData('G', 'J')]
        [InlineData('H', 'A')]
        [InlineData('I', 'Y')]
        [InlineData('J', 'Q')]
        [InlineData('K', 'U')]
        [InlineData('L', 'I')]
        [InlineData('M', 'R')]
        [InlineData('N', 'H')]
        [InlineData('O', 'X')]
        [InlineData('P', 'L')]
        [InlineData('Q', 'N')]
        [InlineData('R', 'F')]
        [InlineData('S', 'T')]
        [InlineData('T', 'G')]
        [InlineData('U', 'K')]
        [InlineData('V', 'D')]
        [InlineData('W', 'C')]
        [InlineData('X', 'M')]
        [InlineData('Y', 'W')]
        [InlineData('Z', 'B')]
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
        [InlineData('B', 'S')]
        [InlineData('C', 'O')]
        [InlineData('D', 'V')]
        [InlineData('E', 'P')]
        [InlineData('F', 'Z')]
        [InlineData('G', 'J')]
        [InlineData('H', 'A')]
        [InlineData('I', 'Y')]
        [InlineData('J', 'Q')]
        [InlineData('K', 'U')]
        [InlineData('L', 'I')]
        [InlineData('M', 'R')]
        [InlineData('N', 'H')]
        [InlineData('O', 'X')]
        [InlineData('P', 'L')]
        [InlineData('Q', 'N')]
        [InlineData('R', 'F')]
        [InlineData('S', 'T')]
        [InlineData('T', 'G')]
        [InlineData('U', 'K')]
        [InlineData('V', 'D')]
        [InlineData('W', 'C')]
        [InlineData('X', 'M')]
        [InlineData('Y', 'W')]
        [InlineData('Z', 'B')]
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
            char expectedNotch = 'R';
            // Act

            // Assert
            Assert.Equal(expectedNotch, rotor.Notch);
        }

        [Fact]
        public void Turnover_PositionSet_CharE_Returned()
        {
            // Arrange
            char expectedTurnover = 'J';
            // Act

            // Assert
            Assert.Equal(expectedTurnover, rotor.Turnover);
        }

        [Theory]
        [InlineData('A', 'C', 'B')]
        [InlineData('F', 'Q', 'B')]
        [InlineData('X', 'D', 'B')]
        [InlineData('Z', 'O', 'C')]
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
        [InlineData('A', 'C', 'A', 'B')]
        [InlineData('B', 'F', 'A', 'B')]
        [InlineData('A', 'E', 'B', 'B')]
        [InlineData('A', 'N', 'Y', 'F')]
        [InlineData('Z', 'A', 'Y', 'F')]
        [InlineData('I', 'Z', 'Y', 'F')]
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
        [InlineData('A', 'G', 'A', 'B')]
        [InlineData('B', 'I', 'A', 'B')]
        [InlineData('A', 'H', 'B', 'B')]
        [InlineData('A', 'Z', 'Y', 'F')]
        [InlineData('F', 'P', 'Y', 'F')]
        [InlineData('Z', 'I', 'Y', 'F')]
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
            rotor = new RotorIV(1);
            int indexTurnover = rotor.GetIndexInputContact(rotor.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexBeforeTurnover = (indexTurnover == 0) ? 25 : indexTurnover - 1;
            char charBeforeTurnover = rotor.Wiring[indexBeforeTurnover, 0];

            rotor.SetPosition(charBeforeTurnover);
            string expectedRotorName = "RotorIV";

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

            mockRotor = new Mock<RotorIV>(null);
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

using EnigmaComponents.Classes;
using Moq;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorV_Tests
    {
        public RotorV_Tests()
        {
            rotor = new RotorV(null);
        }

        char scrambledChar;
        Rotor rotor;
        Mock<RotorV> mockRotor;

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
        public void RotorV_ClassInstantiated_WiringTableCreated()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(rotor.Wiring);
            Assert.Equal(26, rotor.Wiring.GetLength(0));
        }

        [Fact]
        public void RotorV_ClassInstantiated_WiringContainsOnlyValidCharacters()
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

#region public void ScrambleFromRight_GrundstellungA01_CorrectScrambling(char rightChar, char leftChar)
        [Theory]
        [InlineData('A', 'V')]
        [InlineData('B', 'Z')]
        [InlineData('C', 'B')]
        [InlineData('D', 'R')]
        [InlineData('E', 'G')]
        [InlineData('F', 'I')]
        [InlineData('G', 'T')]
        [InlineData('H', 'Y')]
        [InlineData('I', 'U')]
        [InlineData('J', 'P')]
        [InlineData('K', 'S')]
        [InlineData('L', 'D')]
        [InlineData('M', 'N')]
        [InlineData('N', 'H')]
        [InlineData('O', 'L')]
        [InlineData('P', 'X')]
        [InlineData('Q', 'A')]
        [InlineData('R', 'W')]
        [InlineData('S', 'M')]
        [InlineData('T', 'J')]
        [InlineData('U', 'Q')]
        [InlineData('V', 'O')]
        [InlineData('W', 'F')]
        [InlineData('X', 'E')]
        [InlineData('Y', 'C')]
        [InlineData('Z', 'K')]
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
        [InlineData('A', 'V')]
        [InlineData('B', 'Z')]
        [InlineData('C', 'B')]
        [InlineData('D', 'R')]
        [InlineData('E', 'G')]
        [InlineData('F', 'I')]
        [InlineData('G', 'T')]
        [InlineData('H', 'Y')]
        [InlineData('I', 'U')]
        [InlineData('J', 'P')]
        [InlineData('K', 'S')]
        [InlineData('L', 'D')]
        [InlineData('M', 'N')]
        [InlineData('N', 'H')]
        [InlineData('O', 'L')]
        [InlineData('P', 'X')]
        [InlineData('Q', 'A')]
        [InlineData('R', 'W')]
        [InlineData('S', 'M')]
        [InlineData('T', 'J')]
        [InlineData('U', 'Q')]
        [InlineData('V', 'O')]
        [InlineData('W', 'F')]
        [InlineData('X', 'E')]
        [InlineData('Y', 'C')]
        [InlineData('Z', 'K')]
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
        public void Notch_PositionSet_CharH_Returned()
        {
            // Arrange
            char expectedNotch = 'H';
            // Act

            // Assert
            Assert.Equal(expectedNotch, rotor.Notch);
        }

        [Fact]
        public void Turnover_PositionSet_CharZ_Returned()
        {
            // Arrange
            char expectedTurnover = 'Z';
            // Act

            // Assert
            Assert.Equal(expectedTurnover, rotor.Turnover);
        }

        [Theory]
        [InlineData('A', 'L', 'B')]
        [InlineData('F', 'H', 'B')]
        [InlineData('X', 'G', 'B')]
        [InlineData('Z', 'G', 'C')]
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
        [InlineData('A', 'L', 'A', 'B')]
        [InlineData('B', 'W', 'A', 'B')]
        [InlineData('A', 'V', 'B', 'B')]
        [InlineData('A', 'Q', 'Y', 'F')]
        [InlineData('Z', 'T', 'Y', 'F')]
        [InlineData('I', 'G', 'Y', 'F')]
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
        [InlineData('A', 'C', 'A', 'B')]
        [InlineData('B', 'R', 'A', 'B')]
        [InlineData('A', 'Q', 'B', 'B')]
        [InlineData('A', 'N', 'Y', 'F')]
        [InlineData('F', 'O', 'Y', 'F')]
        [InlineData('Z', 'R', 'Y', 'F')]
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
            rotor = new RotorV(1);
            int indexTurnover = rotor.GetIndexInputContact(rotor.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexBeforeTurnover = (indexTurnover == 0) ? 25 : indexTurnover - 1;
            char charBeforeTurnover = rotor.Wiring[indexBeforeTurnover, 0];

            rotor.SetPosition(charBeforeTurnover);
            string expectedRotorName = "RotorV";

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

            mockRotor = new Mock<RotorV>(null);
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

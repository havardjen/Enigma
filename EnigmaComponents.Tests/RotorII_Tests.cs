using EnigmaComponents.Classes;
using Moq;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorII_Tests
    {
        public RotorII_Tests()
        {
            rotor = new RotorII(null);
        }

        char scrambledChar;
        Rotor rotor;
        Mock<RotorII> mockRotor;

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
        public void RotorII_ClassInstantiated_WiringTableCreated()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(rotor.Wiring);
            Assert.Equal(26, rotor.Wiring.GetLength(0));
        }

        [Fact]
        public void RotorII_ClassInstantiated_WiringContainsOnlyValidCharacters()
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
        [InlineData('A', 'A')]
        [InlineData('B', 'J')]
        [InlineData('C', 'D')]
        [InlineData('D', 'K')]
        [InlineData('E', 'S')]
        [InlineData('F', 'I')]
        [InlineData('G', 'R')]
        [InlineData('H', 'U')]
        [InlineData('I', 'X')]
        [InlineData('J', 'B')]
        [InlineData('K', 'L')]
        [InlineData('L', 'H')]
        [InlineData('M', 'W')]
        [InlineData('N', 'T')]
        [InlineData('O', 'M')]
        [InlineData('P', 'C')]
        [InlineData('Q', 'Q')]
        [InlineData('R', 'G')]
        [InlineData('S', 'Z')]
        [InlineData('T', 'N')]
        [InlineData('U', 'P')]
        [InlineData('V', 'Y')]
        [InlineData('W', 'F')]
        [InlineData('X', 'V')]
        [InlineData('Y', 'O')]
        [InlineData('Z', 'E')]
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
        [InlineData('A', 'A')]
        [InlineData('B', 'J')]
        [InlineData('C', 'D')]
        [InlineData('D', 'K')]
        [InlineData('E', 'S')]
        [InlineData('F', 'I')]
        [InlineData('G', 'R')]
        [InlineData('H', 'U')]
        [InlineData('I', 'X')]
        [InlineData('J', 'B')]
        [InlineData('K', 'L')]
        [InlineData('L', 'H')]
        [InlineData('M', 'W')]
        [InlineData('N', 'T')]
        [InlineData('O', 'M')]
        [InlineData('P', 'C')]
        [InlineData('Q', 'Q')]
        [InlineData('R', 'G')]
        [InlineData('S', 'Z')]
        [InlineData('T', 'N')]
        [InlineData('U', 'P')]
        [InlineData('V', 'Y')]
        [InlineData('W', 'F')]
        [InlineData('X', 'V')]
        [InlineData('Y', 'O')]
        [InlineData('Z', 'E')]
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
            char expectedNotch = 'M';
            // Act

            // Assert
            Assert.Equal(expectedNotch, rotor.Notch);
        }

        [Fact]
        public void Turnover_PositionSet_CharE_Returned()
        {
            // Arrange
            char expectedTurnover = 'E';
            // Act

            // Assert
            Assert.Equal(expectedTurnover, rotor.Turnover);
        }

        [Theory]
        [InlineData('A', 'F', 'B')]
        [InlineData('F', 'T', 'B')]
        [InlineData('X', 'G', 'B')]
        [InlineData('Z', 'X', 'C')]
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
        [InlineData('A', 'F', 'A', 'B')]
        [InlineData('B', 'B', 'A', 'B')]
        [InlineData('A', 'A', 'B', 'B')]
        [InlineData('A', 'U', 'Y', 'F')]
        [InlineData('Z', 'G', 'Y', 'F')]
        [InlineData('I', 'Q', 'Y', 'F')]
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
        [InlineData('A', 'T', 'A', 'B')]
        [InlineData('B', 'B', 'A', 'B')]
        [InlineData('A', 'A', 'B', 'B')]
        [InlineData('A', 'U', 'Y', 'F')]
        [InlineData('F', 'C', 'Y', 'F')]
        [InlineData('Z', 'L', 'Y', 'F')]
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
            rotor = new RotorII(1);
            int indexTurnover = rotor.GetIndexInputContact(rotor.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexBeforeTurnover = (indexTurnover == 0) ? 25 : indexTurnover - 1;
            char charBeforeTurnover = rotor.Wiring[indexBeforeTurnover, 0];

            rotor.SetPosition(charBeforeTurnover);
            string expectedRotorName = "RotorII";

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

            mockRotor = new Mock<RotorII>(null);
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

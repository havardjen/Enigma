using EnigmaComponents.Classes;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorI_Tests
    {
        public RotorI_Tests()
        {
            rotor = new RotorI(null);
        }

        Rotor rotor;


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
        public void RotorI_ClassInstantiated_WiringTableCreated()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(rotor.Wiring);
            Assert.Equal(26, rotor.Wiring.GetLength(0));
        }

        [Fact]
        public void RotorI_ClassInstantiated_WiringContainsOnlyValidCharacters()
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

        [Fact]
        public void Notch_PositionSet_CharYReturned()
        {
            // Arrange
            char expectedNotch = 'Y';

            // Act

            // Assert
            Assert.Equal(expectedNotch, rotor.Notch);
        }

        [Fact]
        public void Turnover_PositionSet_CharQReturned()
        {
            // Arrange
            char expectedTurnover = 'Q';

            // Act

            // Assert
            Assert.Equal(expectedTurnover, rotor.Turnover);
        }

        [Fact]
        public void SetRingStellung_SetToB02_RingStellungSet()
        {
            // Arrange
            char ringStellungToSet = 'B';
            int expectedRingStellung = 1; //B-02 is the second position in the list.

            // Act
            rotor.SetRingStellung(ringStellungToSet);

            // Assert
            Assert.Equal(expectedRingStellung, rotor.RingStellung);
        }

        [Fact]
        public void SetPosition_SetToC03_PositionSet()
        {
            // Arrange
            char positionToSet = 'C';
            int expectedPosition = 2;

            // Act
            rotor.SetPosition(positionToSet);

            // Assert
            Assert.Equal(expectedPosition, rotor.Position);
        }

        [Fact]
        public void SetPosition_PositionSetToTurnover_EventRaised()
        {
            // Arrange
            string rotorName = null;

            rotor.RotorTurnoverEvent += delegate (object sender, EventArgs args)
            {
                rotorName = ((RotorTurnoverEventArgs)args).RotorName;
            };

            // Act
            rotor.SetPosition(rotor.Turnover);

            // Assert
            Assert.NotNull(rotorName);
        }

        [Theory]
        [InlineData('A', 'A', 'B', 25)]
        [InlineData('B', 'A', 'A', 1)]
        [InlineData('F', 'A', 'B', 4)]
        [InlineData('B', 'C', 'A', 3)]
        [InlineData('Z', 'A', 'B', 24)]
        [InlineData('Z', 'A', 'C', 23)]
        [InlineData('Z', 'Y', 'F', 18)]
        [InlineData('A', 'F', 'X', 8)]
        public void GetIndexInputContact_PositionSet_CorrectInputContactReturned(char inputPosition, 
                                                                                 char position,
                                                                                 char ringStellung,
                                                                                 int expectedIndex)
        {
            // Arrange
            rotor.SetPosition(position);
            rotor.SetRingStellung(ringStellung);

            // Act
            int indexInputContact = rotor.GetIndexInputContact(inputPosition, rotor.Wiring.GetLength(0) - 1);

            // Assert
            Assert.Equal(expectedIndex, indexInputContact);
        }

        [Theory]
        [InlineData(4, 0, 0, 4)]
        [InlineData(9, 5, 24, 16)]
        [InlineData(17, 2, 0, 19)]
        [InlineData(21, 23, 5, 13)]
        public void GetIndexOutputPosition_indexOutputContactRingStellungPositionSet_CorrectIndexReturned(int outContact, 
                                                                                                          int ringStellung, 
                                                                                                          int position, 
                                                                                                          int expectedOutPosition)
        {
            // Arrange

            // Act
            int indexOutputPosition = rotor.GetIndexOutputPosition(outContact, ringStellung, position);

            // Assert
            Assert.Equal(expectedOutPosition, indexOutputPosition);
        }
    }
}

using EnigmaComponents.Classes;
using Moq;
using System;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class RotorI_Step_Tests
    {
        public RotorI_Step_Tests()
        {
            rotor = new RotorI(null);
        }

        Rotor rotor;
        Mock<RotorI> mockRotor;

        [Theory]
        [InlineData('A')]
        [InlineData('Y')]
        [InlineData('Z')]
        public void HandleRotorTurnoverEvent_RotorTurnoverEventRaised_NotSteppedPropertySet(char currentPosition)
        {
            // Assert
            rotor.SetPosition(currentPosition);

            mockRotor = new Mock<RotorI>(null);
            mockRotor.Object.RotorTurnoverEvent += rotor.RotorTurnoverEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.RotorTurnoverEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            // Act
            mockRotor.Object.Step();

            // Assert
            Assert.Equal(currentPosition, rotor.Wiring[rotor.Position, 0]);
            Assert.True(rotor.PreviousRotorIsAtTurnoverPosition);
        }

        [Fact]
        public void HandleDoubleStepMiddleRotorEvent_DoubleStepMiddleRotorEventRaised_RotorIsStepped()
        {
            // Assert
            rotor = new RotorI(2);      // The rotor under test is the middle rotor.
            char currentPosition = 'A';
            char expectedPosition = 'B';
            rotor.SetPosition(currentPosition);

            // We mock the third rotor:
            mockRotor = new Mock<RotorI>(3);
            mockRotor.Object.DoubleStepMiddleRotorEvent += rotor.DoubleStepMiddleRotorEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.DoubleStepMiddleRotorEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            // Act
            mockRotor.Object.Step(); // This should cause the middle rotor to be stepped(double stepping of middle rotor...).

            // Assert
            Assert.Equal(expectedPosition, rotor.Wiring[rotor.Position, 0]);
        }

        [Theory]
        [InlineData('A', 1)]
        [InlineData('Y', 25)]
        [InlineData('Z', 0)]
        public void Step_ValidInput_RotorSteppedOneStep(char position, int expectedNewPosition)
        {
            // Arrange
            rotor = new RotorI(1);
            rotor.SetPosition(position);

            // Act
            rotor.Step();

            // Assert
            Assert.Equal(expectedNewPosition, rotor.Position);
        }

        [Fact]
        public void Step_MiddleRotorPreviousRotorIsAtTurnoverPositionFalse_RotorNotStepped()
        {
            // Arrange
            char currentPosition = 'A';
            rotor = new RotorI(2);
            rotor.SetPosition(currentPosition);

            // Act
            rotor.Step();

            // Assert
            Assert.Equal(currentPosition, rotor.Wiring[rotor.Position, 0]);
        }

        [Fact]
        public void Step_ThirdRotorPreviousRotorIsAtTurnoverPositionFalse_RotorNotStepped()
        {
            // Arrange
            char currentPosition = 'A';
            rotor = new RotorI(3);
            rotor.SetPosition(currentPosition);

            // Act
            rotor.Step();

            // Assert
            Assert.Equal(currentPosition, rotor.Wiring[rotor.Position, 0]);
        }

        [Fact]
        public void Step_RotorStepsToTurnoverPosition_TurnoverEventIsFired()
        {
            // Arrange
            rotor = new RotorI(1);
            int indexTurnover = rotor.GetIndexInputContact(rotor.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexBeforeTurnover = (indexTurnover == 0) ? 25 : indexTurnover - 1;
            char charBeforeTurnover = rotor.Wiring[indexBeforeTurnover, 0];

            rotor.SetPosition(charBeforeTurnover);
            string expectedRotorName = "RotorI";

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

        [Fact]
        public void Step_PreviousRotorIsAtTurnoverPositionTrue_RotorSteppedPreviousRotorIsAtTurnoverPositionFalse()
        {
            // Assert
            char currentPosition = 'A';
            char newPosition = 'B';
            rotor.SetPosition(currentPosition);

            mockRotor = new Mock<RotorI>(null);
            mockRotor.Object.RotorTurnoverEvent += rotor.RotorTurnoverEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.RotorTurnoverEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            // Act
            mockRotor.Object.Step();
            rotor.Step();

            // Assert
            Assert.Equal(newPosition, rotor.Wiring[rotor.Position, 0]);
            Assert.False(rotor.PreviousRotorIsAtTurnoverPosition);
        }

        [Fact]
        public void Step_MiddleRotorIsAtTurnover_ThirdRotorAlsoStepsMiddleRotor()
        {
            // Arrange
            int rotorNumber = 3;
            rotor = new RotorI(rotorNumber);

            mockRotor = new Mock<RotorI>(2);
            mockRotor.Object.RotorTurnoverEvent += rotor.RotorTurnoverEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.RotorTurnoverEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            string rotorName = null;
            rotor.DoubleStepMiddleRotorEvent += delegate (object sender, EventArgs e)
            {
                rotorName = ((RotorTurnoverEventArgs)e).RotorName;
            };

            // Act
            mockRotor.Object.Step();  // This will set the turnover "indicator" for the rotor.
            rotor.Step();

            // Assert
            Assert.NotNull(rotorName);

        }

        [Fact]
        public void Step_FirstRotorIsAtTurnover_MiddleRotorOnlyStepsItself()
        {
            // Arrange
            int rotorNumber = 2;
            rotor = new RotorI(rotorNumber);

            mockRotor = new Mock<RotorI>(1);
            mockRotor.Object.RotorTurnoverEvent += rotor.RotorTurnoverEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.RotorTurnoverEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            string rotorName = null;
            rotor.DoubleStepMiddleRotorEvent += delegate (object sender, EventArgs e)
            {
                rotorName = ((RotorTurnoverEventArgs)e).RotorName;
            };

            // Act
            mockRotor.Object.Step();  // This will set the turnover "indicator" for the rotor.
            rotor.Step();

            // Assert
            Assert.Null(rotorName);
        }

        [Fact]
        public void Step_RotorDoesNotStepToTurnoverPosition_RotorTurnoverEventNotRaised()
        {
            // Arrange
            rotor = new RotorI(1);
            int indexTurnover = rotor.GetIndexInputContact(rotor.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexAfterTurnover = (indexTurnover == 25) ? 0 : indexTurnover + 1;
            char charAfterTurnover = rotor.Wiring[indexAfterTurnover, 0];

            rotor.SetPosition(charAfterTurnover);
            
            string rotorName = null;
            rotor.RotorTurnoverEvent += delegate (object sender, EventArgs e)
            {
                rotorName = ((RotorTurnoverEventArgs)e).RotorName;
            };

            // Act
            rotor.Step();

            // Assert
            Assert.Null(rotorName);
        }

    }
}

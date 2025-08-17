using EnigmaResources.ViewModels;
using EnigmaComponents.Classes;
using System.Collections.Generic;
using Xunit;
using Moq;
using EnigmaComponents;
using EnigmaComponents.Interfaces;

namespace EnigmaResources.Tests
{
    public class MainWindowViewModelTests
    {
        public MainWindowViewModelTests()
        {
            _brett = new Steckerbrett();

            _viewModel = new MainWindowViewModel(_brett);
            walzenlageToSet = new List<string> { "III", "II", "I" };
            _viewModel.SetWalzenlage(walzenlageToSet);
        }

        private MainWindowViewModel _viewModel;
        private ISteckerbrett _brett;
        private List<string> walzenlageToSet;


        [Fact]
        public void SetWalzenlage_ValidWalzenlageSupplied_WalzenlageSet()
        {
            // Arrange
            
            // Act
            // Done in constructor.
            
            // Assert
            Assert.NotNull(_viewModel.WalzenLage);
            Assert.IsType<RotorIII>(_viewModel.WalzenLage[0]);
            Assert.IsType<RotorII>(_viewModel.WalzenLage[1]);
            Assert.IsType<RotorI>(_viewModel.WalzenLage[2]);
        }

        [Fact]
        public void StepRotors_RightRotorStepsToTurnover_MiddleRotorNotified()
        {
            // Arrange
            Rotor rotor = _viewModel.WalzenLage[0];

            char charBeforeTurnover = SetRotorPositionBeforeTurnover(rotor);
            rotor.SetPosition(charBeforeTurnover);

            // Act
            _viewModel.StepRotors();
            bool newMiddlePreviousIsTurnover = _viewModel.WalzenLage[1].PreviousRotorIsAtTurnoverPosition;

            // Assert
            Assert.True(newMiddlePreviousIsTurnover);
        }

        [Fact]
        public void StepRotors_ThirdRotorPreviousIsAtTurnover_MiddleRotorIsStepped()
        {
            // This test verifies that the stepping method mimics the double stepping anomaly
            //  the real Enigmas suffered from.

            // Arrange
            Mock<RotorII> mockRotor = new Mock<RotorII>(2);
            Rotor rotor = _viewModel.WalzenLage[1];
            char middleBeforeTurnover = SetRotorPositionBeforeTurnover(rotor);
            rotor.SetPosition(middleBeforeTurnover);

            mockRotor.Object.RotorTurnoverEvent += rotor.RotorTurnoverEventHandler;
            mockRotor.Setup(m => m.Step()).Raises(m => m.RotorTurnoverEvent += null, new RotorTurnoverEventArgs("MockRotor"));

            // This will set PreviousRotorIsAtTurnoverPosition to true for the middle rotor
            mockRotor.Object.Step();

            // Act
            _viewModel.StepRotors(); // Middle rotor steps and Rotor 3 is now notified to ("double") step middle rotor next time.
            int middlePosition = rotor.Position;
            int expectedPosition = (rotor.Position == 25) ? 0 : rotor.Position + 1;
            _viewModel.StepRotors();

            // Assert
            Assert.Equal(expectedPosition, _viewModel.WalzenLage[1].Position);
        }

        [Fact]
        public void SetPositions_ValidInput_PositionsAreSet()
        {
            // Arrange
            RotorI rotor = new RotorI(null);
            string positionsToSet = "YCM";
            List<int> expectedIndexes = new List<int> { rotor.GetIndexInputContact(positionsToSet.Substring(0,1).ToCharArray()[0], 25),
                                                        rotor.GetIndexInputContact(positionsToSet.Substring(1,1).ToCharArray()[0], 25),
                                                        rotor.GetIndexInputContact(positionsToSet.Substring(2,1).ToCharArray()[0], 25)
                                                      };

            // Act
            _viewModel.SetPositions(positionsToSet);

            // Assert
            Assert.Equal(expectedIndexes[0], _viewModel.WalzenLage[0].Position);
            Assert.Equal(expectedIndexes[1], _viewModel.WalzenLage[1].Position);
            Assert.Equal(expectedIndexes[2], _viewModel.WalzenLage[2].Position);
        }

        [Fact]
        public void SetRingstellungs_ValidInput_RingStellungsSet()
        {
            // Arrange
            RotorI rotor = new RotorI(null);
            string ringStellungsToSet = "BFY";
            List<int> expectedIndexes = new List<int> { rotor.GetIndexInputContact(ringStellungsToSet.Substring(0,1).ToCharArray()[0], 25),
                                                        rotor.GetIndexInputContact(ringStellungsToSet.Substring(1,1).ToCharArray()[0], 25),
                                                        rotor.GetIndexInputContact(ringStellungsToSet.Substring(2,1).ToCharArray()[0], 25)
                                                      };

            // Act
            _viewModel.SetRingStellungen(ringStellungsToSet);

            // Assert
            Assert.Equal(expectedIndexes[0], _viewModel.WalzenLage[0].RingStellung);
            Assert.Equal(expectedIndexes[1], _viewModel.WalzenLage[1].RingStellung);
            Assert.Equal(expectedIndexes[2], _viewModel.WalzenLage[2].RingStellung);
        }

        [Theory]
        [InlineData('A', 'B', "AAA", "AAA", "III|II|I")]
        [InlineData('A', 'C', "AAA", "AAA", "II|I|III")]
        [InlineData('A', 'X', "FAW", "XCZ", "I|III|II")]
        [InlineData('L', 'A', "GZA", "RVA", "I|III|II")]
        public void EncodeCharacter_SingleCharacterInput_CorrectEncoding(char inputChar, char expectedChar, string ringStellungs, string positions, string walzenlage)
        {
            // Arrange
            string[] lage = walzenlage.Split('|');
            walzenlageToSet = new List<string> { lage[0], lage[1], lage[2] };

            _viewModel.SetWalzenlage(walzenlageToSet);
            _viewModel.SetRingStellungen(ringStellungs);
            _viewModel.SetPositions(positions);

            // Act
            char encodedChar = _viewModel.EncodeCharacter(inputChar);

            // Assert
            Assert.Equal(expectedChar, encodedChar);
        }

        [Theory]
        [InlineData("AAAAAAAAAA", "BDZGOWCXLT", "AAA", "AAA", "III|II|I")]
        [InlineData("KAROLINE", "WDXMULRY", "AAA", "AAA", "III|II|I")]
        [InlineData("HEI", "ILG", "AAA", "AAA", "III|II|I")]
        [InlineData("HEI", "KZX", "AAA", "AAA", "II|I|III")]
        [InlineData("HEI", "NKK", "FAW", "XCZ", "I|III|II")]
        public void EncodeCharacter_StringInput_CorrectEncodedString(string input, string expectedEncoded, string ringStellungs, string positions, string walzenlage)
        {
            // Arrange
            string result = string.Empty;
            string[] lage = walzenlage.Split('|');
            walzenlageToSet = new List<string> { lage[0], lage[1], lage[2] };

            _viewModel.SetWalzenlage(walzenlageToSet);
            _viewModel.SetRingStellungen(ringStellungs);
            _viewModel.SetPositions(positions);

            // Act
            foreach (char c in input.ToCharArray())
            {
                result += _viewModel.EncodeCharacter(c).ToString();
            }

            // Assert
            Assert.Equal(expectedEncoded, result);
        }

        [Fact]
        public void EncodeCharacter_LongMessage_CorrectEncodedMessage()
        {
            // Arrange
            string messageToEncode = "DETTEXERXENXLANGXMELDINGXJEGXLURERXPAAXOMXDENXBLIRXKODETXRIKTIGXSKYENEXERXMANGEXNAARXSOLAXIKKEXSKINNERXHVORXMANGEXKOMMERXPAAXFESTXIXKVELDXMONXTROX";
            string expectedEncoded = "UIGUDVYVEVZQAELNUKBNSMPSGUBTUXWHJYJDBGDDZKUXIMHIXWRQPFAWGGFCOGEDRYIUTGKITTSFZMPQTKNKJFRWXSHHVVDLCCFXXLWZFGYROWXCAYHNBIOFPSPPMNFOXNLSFIWFNBYHMOCYSM";
            string result = string.Empty;
            List<string> walzenlage = new List<string>() { "II", "I", "III" };
            string positions = "TQZ";
            string ringstellungs = "YGM";

            _viewModel.SetWalzenlage(walzenlage);
            _viewModel.SetRingStellungen(ringstellungs);
            _viewModel.SetPositions(positions);

            // Act
            foreach (char c in messageToEncode.ToCharArray())
            {
                result += _viewModel.EncodeCharacter(c).ToString();
            }

            // Assert
            Assert.Equal(expectedEncoded, result);
        }


        private char SetRotorPositionBeforeTurnover(Rotor rotorToStep)
        {
            int indexTurnover = rotorToStep.GetIndexInputContact(rotorToStep.Turnover, 25); //TODO: Build an alphabet List<char> locally, and NOT use rotor's wiring.
            int indexBeforeTurnover = (indexTurnover == 0) ? 25 : indexTurnover - 1;
            char charBeforeTurnover = rotorToStep.Wiring[indexBeforeTurnover, 0];

            return charBeforeTurnover;
        }
    }
}

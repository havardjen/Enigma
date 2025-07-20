using EnigmaComponents.Classes;
using EnigmaComponents.Interfaces;
using EnigmaResources.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace EnigmaResources.Tests
{
    public class MainVM_Steckered_Tests
    {
        public MainVM_Steckered_Tests()
        {
            brett = new Steckerbrett();
            viewModel = new MainWindowViewModel(brett);

            walzenlageToSet = new List<string> { "III", "II", "I" };
            viewModel.SetWalzenlage(walzenlageToSet);
        }

        ISteckerbrett brett;
        MainWindowViewModel viewModel;
        List<string> walzenlageToSet;

        [Fact]
        public void SetPluggings_CorrectInput_ExpectedEncodings()
        {
            // Arrange
            Dictionary<char, char> pluggings = new Dictionary<char, char>
            {
                { 'A', 'G' },
                { 'Z', 'C' },
                { 'Y', 'B' },
                { 'M', 'X' },
                { 'F', 'Q' }
            };
            char charToEncode = 'A';
            char expectedEncoded = 'M';

            // Act
            viewModel.SetPluggings(pluggings);
            char result = viewModel.EncodeCharacter(charToEncode);

            // Assert
            Assert.Equal(expectedEncoded, result);
        }
    }
}

using EnigmaResources.ViewModels;
using System;
using Xunit;

namespace EnigmaResources.Tests
{
    public class KeyboardViewModelTests
    {
        public KeyboardViewModelTests()
        {
            _keyboardViewModel = new KeyboardViewModel();
        }

        private KeyboardViewModel  _keyboardViewModel;

        [Theory]
        [InlineData("A")]
        [InlineData("M")]
        [InlineData("Z")]
        public void KeyPressedCommandHandler_ValidKeyIsPressed_EventIsRaised(string actualKeyPressed)
        {
            // Arrange
            string resultingKeyPressed = null;
            _keyboardViewModel.KeyPressedEvent += delegate (object sender, EventArgs e)
            {
                resultingKeyPressed = ((KeyPressedEventArgs)e).KeyPressed;
            };

            // Act
            _keyboardViewModel.KeyPressedCommandHandler(actualKeyPressed);

            // Assert
            Assert.NotNull(resultingKeyPressed);
            Assert.Equal(actualKeyPressed, resultingKeyPressed);
        }
    }
}

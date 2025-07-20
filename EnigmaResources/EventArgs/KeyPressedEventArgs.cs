using System;

namespace EnigmaResources
{
    public class KeyPressedEventArgs : EventArgs
    {
        public KeyPressedEventArgs(string keyPressed)
        {
            KeyPressed = keyPressed;
        }

        public string KeyPressed { get; private set; }
    }
}

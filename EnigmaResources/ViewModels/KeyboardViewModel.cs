using System;

namespace EnigmaResources.ViewModels
{
    public class KeyboardViewModel
    {
        public KeyboardViewModel()
        {

        }


        public event EventHandler KeyPressedEvent = delegate { };

        public void KeyPressedCommandHandler(object keyPressed)
        {
            KeyPressedEventArgs e = new KeyPressedEventArgs(keyPressed.ToString());

            KeyPressedEvent(this, e);
        }
    }
}

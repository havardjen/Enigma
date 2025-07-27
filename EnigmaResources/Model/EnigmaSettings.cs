using EnigmaComponents.Classes;
using System.Collections.Generic;

namespace EnigmaResources.Model
{
    public class EnigmaSettings
    {
        public EnigmaSettings(List<RotorSettings> rotorSettings, string message)
        {
            Rotors = new RotorSettings[rotorSettings.Count];
            Message = message;
        }


        public string Message { get; private set; }
        public RotorSettings[]  Rotors { get; private set; }
        public int NumRotors { get { return Rotors.Length; } }
    }
}

using EnigmaComponents.Classes;
using System.Collections.Generic;

namespace EnigmaResources.Model
{
    public class EnigmaSettingsDto
    {
        public string Message { get; set; }
        public RotorSettingsDto[]  Rotors { get; set; }
        public int NumRotors { get { return Rotors.Length; } }
    }
}

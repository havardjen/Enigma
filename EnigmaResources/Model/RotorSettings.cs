using EnigmaComponents.Classes;

namespace EnigmaResources.Model
{
    public class RotorSettings
    {
        public RotorSettings(string rotorName, string ringstellung, string position)
        {
            RotorName = rotorName;
            Ringstellung = ringstellung;
            Position = position;
        }

        public string RotorName { get; set; }

        public string Ringstellung { get; set; }

        public string Position { get; set; }
    }
}

using EnigmaComponents.Classes;

namespace EnigmaResources.Model
{
    public class RotorSettings
    {
        public RotorSettings(Rotor rotor, string ringstellung, string position)
        {
            Rotor = rotor;
            Ringstellung = ringstellung;
            Position = position;
        }

        public Rotor Rotor { get; private set; }
        public string Ringstellung { get; private set; }
        public string Position { get; private set; }
    }
}

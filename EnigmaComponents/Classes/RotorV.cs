
namespace EnigmaComponents.Classes
{
    public class RotorV : Rotor
    {
        public RotorV(int? rotorNumber)
            :base("RotorV", rotorNumber)
        {
            RingStellung = 0;
            Position = 0;
            Wiring = GetWiringTable();
            Notch = 'H';
            Turnover = 'Z';
        }

        public override char[,] Wiring { get; }
        public override char Notch { get; }
        public override char Turnover { get; }

        private char[,] GetWiringTable()
        {
            return new char[26, 2]
            {
                { 'A', 'V' },
                { 'B', 'Z' },
                { 'C', 'B' },
                { 'D', 'R' },
                { 'E', 'G' },
                { 'F', 'I' },
                { 'G', 'T' },
                { 'H', 'Y' },
                { 'I', 'U' },
                { 'J', 'P' },
                { 'K', 'S' },
                { 'L', 'D' },
                { 'M', 'N' },
                { 'N', 'H' },
                { 'O', 'L' },
                { 'P', 'X' },
                { 'Q', 'A' },
                { 'R', 'W' },
                { 'S', 'M' },
                { 'T', 'J' },
                { 'U', 'Q' },
                { 'V', 'O' },
                { 'W', 'F' },
                { 'X', 'E' },
                { 'Y', 'C' },
                { 'Z', 'K'}
            };
        }
    }
}



namespace EnigmaComponents.Classes
{
    public class RotorIV     : Rotor
    {
        public RotorIV(int? rotorNumber)
            :base("RotorIV", rotorNumber)
        {
            RingStellung = 0;
            Position = 0;
            Wiring = GetWiringTable();
            Notch = 'R';
            Turnover = 'J';
        }

        public override char[,] Wiring { get; }
        public override char Notch { get; }
        public override char Turnover { get; }

        private char[,] GetWiringTable()
        {
            return new char[26, 2]
            {
                { 'A', 'E' },
                { 'B', 'S' },
                { 'C', 'O' },
                { 'D', 'V' },
                { 'E', 'P' },
                { 'F', 'Z' },
                { 'G', 'J' },
                { 'H', 'A' },
                { 'I', 'Y' },
                { 'J', 'Q' },
                { 'K', 'U' },
                { 'L', 'I' },
                { 'M', 'R' },
                { 'N', 'H' },
                { 'O', 'X' },
                { 'P', 'L' },
                { 'Q', 'N' },
                { 'R', 'F' },
                { 'S', 'T' },
                { 'T', 'G' },
                { 'U', 'K' },
                { 'V', 'D' },
                { 'W', 'C' },
                { 'X', 'M' },
                { 'Y', 'W' },
                { 'Z', 'B'}
             };
        }
    }
}


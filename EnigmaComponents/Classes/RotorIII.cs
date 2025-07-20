
namespace EnigmaComponents.Classes
{
    public class RotorIII : Rotor
    {
        public RotorIII(int? rotorNumber)
            :base("RotorIII", rotorNumber)
        {
            RingStellung = 0;
            Position = 0;
            Wiring = GetWiringTable();
            Notch = 'D';
            Turnover = 'V';
        }

        public override char[,] Wiring { get; }
        public override char Notch { get; }
        public override char Turnover { get; }

        private char[,] GetWiringTable()
        {
            return new char[26, 2]
            {
                { 'A', 'B' },
                { 'B', 'D' },
                { 'C', 'F' },
                { 'D', 'H' },
                { 'E', 'J' },
                { 'F', 'L' },
                { 'G', 'C' },
                { 'H', 'P' },
                { 'I', 'R' },
                { 'J', 'T' },
                { 'K', 'X' },
                { 'L', 'V' },
                { 'M', 'Z' },
                { 'N', 'N' },
                { 'O', 'Y' },
                { 'P', 'E' },
                { 'Q', 'I' },
                { 'R', 'W' },
                { 'S', 'G' },
                { 'T', 'A' },
                { 'U', 'K' },
                { 'V', 'M' },
                { 'W', 'U' },
                { 'X', 'S' },
                { 'Y', 'Q' },
                { 'Z', 'O'}
             };
        }
    }
}

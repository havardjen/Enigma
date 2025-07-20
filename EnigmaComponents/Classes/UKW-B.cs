
namespace EnigmaComponents.Classes
{
    public class UKW_B : Umkehrwalze
    {
        public UKW_B()
        {
            Wiring = SetWiring();
        }
        public override char[,] Wiring { get; }

        private char[,] SetWiring()
        {
            return new char[26, 2]
            {
                { 'A', 'Y' },
                { 'B', 'R' },
                { 'C', 'U' },
                { 'D', 'H' },
                { 'E', 'Q' },
                { 'F', 'S' },
                { 'G', 'L' },
                { 'H', 'D' },
                { 'I', 'P' },
                { 'J', 'X' },
                { 'K', 'N' },
                { 'L', 'G' },
                { 'M', 'O' },
                { 'N', 'K' },
                { 'O', 'M' },
                { 'P', 'I' },
                { 'Q', 'E' },
                { 'R', 'B' },
                { 'S', 'F' },
                { 'T', 'Z' },
                { 'U', 'C' },
                { 'V', 'W' },
                { 'W', 'V' },
                { 'X', 'J' },
                { 'Y', 'A' },
                { 'Z', 'T' }
            };
        }
    }
}

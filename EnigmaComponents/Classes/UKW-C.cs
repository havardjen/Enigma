
namespace EnigmaComponents.Classes
{
    public class UKW_C : Umkehrwalze
    {
        public UKW_C()
        {
            Wiring = SetWiring();
        }
        public override char[,] Wiring { get; }

        private char[,] SetWiring()
        {
            return new char[26, 2]
            {
                { 'A', 'F' },
                { 'B', 'V' },
                { 'C', 'P' },
                { 'D', 'J' },
                { 'E', 'I' },
                { 'F', 'A' },
                { 'G', 'O' },
                { 'H', 'Y' },
                { 'I', 'E' },
                { 'J', 'D' },
                { 'K', 'R' },
                { 'L', 'Z' },
                { 'M', 'X' },
                { 'N', 'W' },
                { 'O', 'G' },
                { 'P', 'C' },
                { 'Q', 'T' },
                { 'R', 'K' },
                { 'S', 'U' },
                { 'T', 'Q' },
                { 'U', 'S' },
                { 'V', 'B' },
                { 'W', 'N' },
                { 'X', 'M' },
                { 'Y', 'H' },
                { 'Z', 'L' }
            };
        }
    }
}

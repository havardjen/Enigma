
namespace EnigmaComponents.Classes
{
    public class RotorII : Rotor
    {
        public RotorII(int? rotorNumber)
            :base("RotorII", rotorNumber)
        {
            RingStellung = 0;
            Position = 0;
            Wiring = GetWiringTable();
            Notch = 'M';
            Turnover = 'E';
        }

        public override char[,] Wiring { get; }
        public override char Notch { get; }
        public override char Turnover { get; }

        private char[,] GetWiringTable()
        {
            return new char[26, 2]
            {
                { 'A','A'},
                { 'B','J'},
                { 'C','D'},
                { 'D','K'},
                { 'E','S'},
                { 'F','I'},
                { 'G','R'},
                { 'H','U'},
                { 'I','X'},
                { 'J','B'},
                { 'K','L'},
                { 'L','H'},
                { 'M','W'},
                { 'N','T'},
                { 'O','M'},
                { 'P','C'},
                { 'Q','Q'},
                { 'R','G'},
                { 'S','Z'},
                { 'T','N'},
                { 'U','P'},
                { 'V','Y'},
                { 'W','F'},
                { 'X','V'},
                { 'Y','O'},
                { 'Z','E'}
             };
        }
    }
}

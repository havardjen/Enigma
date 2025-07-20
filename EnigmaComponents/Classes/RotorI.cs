
namespace EnigmaComponents.Classes
{
    public class RotorI : Rotor
    {
        public RotorI(int? rotorNumber)
            :base("RotorI", rotorNumber)
        {
            RingStellung = 0;
            Position = 0;
            Wiring = GetWiringTable();
            Notch = 'Y';
            Turnover = 'Q';
        }

        public override char[,] Wiring { get; }
        public override char Notch { get; }
        public override char Turnover { get; }

        private char[,] GetWiringTable()
        {
            return new char[26, 2]
            {
                { 'A','E'},
                { 'B','K'},
                { 'C','M'},
                { 'D','F'},
                { 'E','L'},
                { 'F','G'},
                { 'G','D'},
                { 'H','Q'},
                { 'I','V'},
                { 'J','Z'},
                { 'K','N'},
                { 'L','T'},
                { 'M','O'},
                { 'N','W'},
                { 'O','Y'},
                { 'P','H'},
                { 'Q','X'},
                { 'R','U'},
                { 'S','S'},
                { 'T','P'},
                { 'U','A'},
                { 'V','I'},
                { 'W','B'},
                { 'X','R'},
                { 'Y','C'},
                { 'Z','J'}
             };
        }
    }
}

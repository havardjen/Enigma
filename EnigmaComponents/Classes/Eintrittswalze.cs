
namespace EnigmaComponents.Classes
{
    public class Eintrittswalze
    {
        public Eintrittswalze()
        {
            Wiring = GetWiringTable();
        }

        public char[,] Wiring { get; }
        
        private char[,] GetWiringTable()
        {
            return new char[26, 2]
            {
                { 'A','A'},
                { 'B','B'},
                { 'C','C'},
                { 'D','D'},
                { 'E','E'},
                { 'F','F'},
                { 'G','G'},
                { 'H','H'},
                { 'I','I'},
                { 'J','J'},
                { 'K','K'},
                { 'L','L'},
                { 'M','M'},
                { 'N','N'},
                { 'O','O'},
                { 'P','P'},
                { 'Q','Q'},
                { 'R','R'},
                { 'S','S'},
                { 'T','T'},
                { 'U','U'},
                { 'V','V'},
                { 'W','W'},
                { 'X','X'},
                { 'Y','Y'},
                { 'Z','Z'}
             };
        }

        public int GetIndexInputChar(char inputChar, bool fromRight = true)
        {
            int resultingIndex = 99;
            int charNumber;

            if (fromRight)
                charNumber = 0;
            else
                charNumber = 1;

            for(int i=0; i<Wiring.GetLength(0); i++)
            {
                if(Wiring[i,charNumber] == inputChar)
                {
                    resultingIndex = i;
                    break;
                }
            }

            return resultingIndex;
        }

        public char GetLeftChar(char rightChar)
        {
            int indexRightChar = GetIndexInputChar(rightChar);

            char leftChar = Wiring[indexRightChar, 1];

            return leftChar;
        }

        public char GetRightChar(char leftChar)
        {
            int indeLeftChar = GetIndexInputChar(leftChar, false);

            char rightChar = Wiring[indeLeftChar, 0];

            return rightChar;
        }
    }
}

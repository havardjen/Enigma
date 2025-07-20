using EnigmaComponents.Exceptions;
using EnigmaComponents.Interfaces;

namespace EnigmaComponents.Classes
{
    public class Steckerbrett : ISteckerbrett
    {
        public Steckerbrett()
        {
            Plugging = new char[26, 2];
            InitPlugging();
        }

        private char[] alphabet;

        public char[,] Plugging { get; }

        private void InitPlugging()
        {
            alphabet = Helper.GetAlphabetArray();

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Plugging[i, j] = alphabet[i];
                }
            }

        }

        private int GetIndexOfDialledCharacter(char dialledCharacter)
        {
            int indexToReturn = 0;

            for (int i = 0; i < 26; i++)
            {
                if (Plugging[i, 0] == dialledCharacter)
                {
                    indexToReturn = i;
                    break;
                }
            }

            return indexToReturn;
        }

        private int GetIndexOfEncodedCharacter(char encodedCharacter)
        {
            int indexToReturn = 0;

            for (int i = 0; i < 26; i++)
            {
                if (Plugging[i, 1] == encodedCharacter)
                {
                    indexToReturn = i;
                    break;
                }
            }

            return indexToReturn;
        }

        private bool PluggingIsValid(char from, char to, int indFrom, int indTo)
        {
            return (Plugging[indFrom, 1] == to &&
                    Plugging[indTo, 1] == from);
        }

        private bool PlugIstaken(char charToCheck)
        {
            bool isTaken = false;

            int indToCheck = GetIndexOfDialledCharacter(charToCheck);

            if(Plugging[indToCheck,0] != Plugging[indToCheck,1])
            {
                isTaken = true;
            }

            return isTaken;
        }

        public char EncodeCharFromKeyboard(char dialledChar)
        {
            int indexOfDialledChar = GetIndexOfDialledCharacter(dialledChar);

            return Plugging[indexOfDialledChar, 1];
        }

        public char EncodeCharFromRotors(char encodedChar)
        {
            int indexOfEncodedChar = GetIndexOfEncodedCharacter(encodedChar);

            return Plugging[indexOfEncodedChar, 0];
        }

        public void InsertCable(char from, char to)
        {
            int indFrom = GetIndexOfDialledCharacter(from);
            int indTo = GetIndexOfDialledCharacter(to);
            bool fromIsTaken = PlugIstaken(from);
            bool toIsTaken = PlugIstaken(to);

            if(fromIsTaken || toIsTaken)
            {
                throw new PlugTakenException();
            }

            Plugging[indFrom, 1] = to;
            Plugging[indTo, 1] = from;
        }

        public void RemoveCable(char from, char to)
        {
            int indFrom = GetIndexOfDialledCharacter(from);
            int indTo = GetIndexOfDialledCharacter(to);
            bool isValidPlugging = PluggingIsValid(from, to, indFrom, indTo);
            
            if(!isValidPlugging)
            {
                throw new InvalidPluggingException();
            }
            else
            {
                Plugging[indFrom, 1] = from;
                Plugging[indTo, 1] = to;
            }
        }
    }
}

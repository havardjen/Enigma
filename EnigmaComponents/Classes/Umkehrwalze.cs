
namespace EnigmaComponents.Classes
{
    public abstract class Umkehrwalze
    {
        public abstract char[,] Wiring { get; }

        public int GetIndexInputChar(char input)
        {
            int result = 99;

            for(int i=0; i<Wiring.GetLength(0); i++)
            {
                if(Wiring[i,0] == input)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public char ReflectChar(char input)
        {
            char result = char.MinValue;
            int indexInputChar = GetIndexInputChar(input);
            result = Wiring[indexInputChar, 1];

            return result;
        }
    }
}

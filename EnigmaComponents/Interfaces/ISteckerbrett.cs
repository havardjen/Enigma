
namespace EnigmaComponents.Interfaces
{
    public interface ISteckerbrett
    {
        char[,] Plugging { get; }

        char EncodeCharFromKeyboard(char dialledChar);
        char EncodeCharFromRotors(char encodedChar);
        void InsertCable(char from, char to);
        void RemoveCable(char from, char to);
    }
}

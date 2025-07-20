using EnigmaComponents.Classes;
using EnigmaComponents.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace EnigmaResources.ViewModels
{
	/// <summary>
	/// I deliberately chose to use some german names and expressions.
	/// </summary>
    public class MainWindowViewModel : Notifier
    {
        public MainWindowViewModel(ISteckerbrett brett)
        {
            _brett = brett;
            etw = new Eintrittswalze();
            SetWalzenlage(new List<string> { "III", "II", "I" });
            ukw = new UKW_B();

            InitRotorList();
            InitAlphabet();
            InitCommands();

            AdvancedIsChecked = false;
        }

        private List<Rotor> _walzenLage;
        private ISteckerbrett _brett;
        private Eintrittswalze etw;
        private Umkehrwalze ukw;
        private bool _advancedIsChecked;

#region Properties

		/// <summary>
		/// The rotors are added from right to left, seen from the operator's perspective.
		/// </summary>
		public List<Rotor> WalzenLage
        {
            get { return _walzenLage;}
        }

        public List<string> Rotors { get; private set; }
        public int SelectedRotor3Index { get; set; }
        public int SelectedRotor2Index { get; set; }
        public int SelectedRotor1Index { get; set; }
        public List<string> Alphabet { get; private set; }
        public int Rotor3RingStellung { get; set; }
        public int Rotor2RingStellung { get; set; }
        public int Rotor1RingStellung { get; set; }
        public int Rotor3Position
        {
            get { return _walzenLage[2].Position; }
            set { _walzenLage[2].Position = value; }
        }
        public int Rotor2Position
        {
            get { return _walzenLage[1].Position; }
            set { _walzenLage[1].Position = value; }
        }
        public int Rotor1Position
        {
            get { return _walzenLage[0].Position; }
            set { _walzenLage[0].Position = value; }
        }
        public string OriginalMessage { get; set; }
        public string CodedDecodedMessage { get; set; }
        public bool AdvancedIsChecked
        {
            get { return _advancedIsChecked; }
            set
            {
                _advancedIsChecked = value;
                OriginalMessage = string.Empty;

                if (_advancedIsChecked)
                {
                    AdvancedVisibility = Visibility.Visible;
                    KeyboardVisibility = Visibility.Collapsed;
                }
                else
                {
                    AdvancedVisibility = Visibility.Collapsed;
                    KeyboardVisibility = Visibility.Visible;
                }

                OnPropertyChanged("AdvancedVisibility");
                OnPropertyChanged("KeyboardVisibility");
                OnPropertyChanged("AdvancedIsChecked");
                OnPropertyChanged("OriginalMessage");
                OnPropertyChanged("OriginalMessageGUIReadOnly");
            }
        }
        public Visibility AdvancedVisibility { get; set; }
        public Visibility KeyboardVisibility { get; set; }
        public bool OriginalMessageGUIReadOnly
        {
            get { return !AdvancedIsChecked; }
        }

        public GeneralNoParameterCommand SetRotorSettingsCommand { get; set; }
        public GeneralNoParameterCommand CodeDecodeMessageCommand { get; set; }

#endregion

		public void SetPluggings(Dictionary<char,char> pluggings)
        {
            foreach(KeyValuePair<char,char> kvp in pluggings)
            {
                _brett.InsertCable(kvp.Key, kvp.Value);
            }
        }

        public void SetPositions(string positions)
        {
            char[] input = positions.ToCharArray();

            _walzenLage[0].SetPosition(input[0]);
            _walzenLage[1].SetPosition(input[1]);
            _walzenLage[2].SetPosition(input[2]);
        }

        public void SetRingStellungs(string ringStellungs)
        {
            char[] input = ringStellungs.ToCharArray();

            _walzenLage[0].SetRingStellung(input[0]);
            _walzenLage[1].SetRingStellung(input[1]);
            _walzenLage[2].SetRingStellung(input[2]);
        }

        public void SetWalzenlage(List<string> lageToSet = null)
        {
            _walzenLage = new List<Rotor>();

            // This method can be called without input when initializing MainWindowViewModel
            // If so, we can't run the rest of the method with empty input:

            if (lageToSet == null)
                return;

			// The rotors are added from right to left, seen from the operator's perspective.
            // https://www.cryptomuseum.com/crypto/enigma/working.htm
            foreach(string walzeNumber in lageToSet)
            {
                switch(walzeNumber)
                {
                    case "I":
                        _walzenLage.Add(new RotorI(_walzenLage.Count + 1));
                        break;
                    case "II":
                        _walzenLage.Add(new RotorII(_walzenLage.Count + 1));
                        break;
                    case "III":
                        _walzenLage.Add(new RotorIII(_walzenLage.Count + 1));
                        break;
                    case "IV":
                        _walzenLage.Add(new RotorIV(_walzenLage.Count + 1));
                        break;
                    case "V":
                        _walzenLage.Add(new RotorV(_walzenLage.Count + 1));
                        break;
                    default:
                        break;
                }
            }

            _walzenLage[0].RotorTurnoverEvent += _walzenLage[1].RotorTurnoverEventHandler; //When Rotor 1 is on turnover position...
            _walzenLage[1].RotorTurnoverEvent += _walzenLage[2].RotorTurnoverEventHandler;
            _walzenLage[2].DoubleStepMiddleRotorEvent += _walzenLage[1].DoubleStepMiddleRotorEventHandler;
        }

        public void StepRotors()
        {
			// The original mechanical Enigma machine suffered from the Double stepping anomaly.
			// This way of ordering the stepping of rotors was chosen to also be able to emulate double stepping of the middle rotor.
            _walzenLage[2].Step();
            _walzenLage[1].Step();
            _walzenLage[0].Step();

            OnPropertyChanged("Rotor3Position");
            OnPropertyChanged("Rotor2Position");
            OnPropertyChanged("Rotor1Position");
        }

        public char EncodeCharacter(char inputChar)
        {
            StepRotors();

            char encodedChar = _brett.EncodeCharFromKeyboard(inputChar);
            encodedChar = etw.GetLeftChar(encodedChar);

            for (int i = 0; i < _walzenLage.Count; i++)
                encodedChar = _walzenLage[i].ScrambleFromRight(encodedChar);
            
            encodedChar = ukw.ReflectChar(encodedChar);

            for (int i = _walzenLage.Count - 1; i >= 0; i--)
                encodedChar = _walzenLage[i].ScrambleFromLeft(encodedChar);

            encodedChar = etw.GetRightChar(encodedChar);
            encodedChar = _brett.EncodeCharFromRotors(encodedChar);

            return encodedChar;
        }



        private void InitRotorList()
        {
            Rotor rI   = new RotorI(null);
            Rotor rII  = new RotorII(null);
            Rotor rIII = new RotorIII(null);
            Rotor rIV  = new RotorIV(null);
            Rotor rV   = new RotorV(null);

            Rotors = new List<string>();
            Rotors.Add(rI.RotorName.Substring(rI.RotorName.IndexOf('I')));
            Rotors.Add(rII.RotorName.Substring(rII.RotorName.IndexOf('I')));
            Rotors.Add(rIII.RotorName.Substring(rIII.RotorName.IndexOf('I')));
            Rotors.Add(rIV.RotorName.Substring(rIV.RotorName.IndexOf('I')));
            Rotors.Add(rV.RotorName.Substring(rV.RotorName.IndexOf('V')));

            OnPropertyChanged("Rotors");
        }

        private void InitAlphabet()
        {
            char[] tmpAlphabet = Helper.GetAlphabetArray();
            Alphabet = new List<string>();

            foreach (char c in tmpAlphabet)
                Alphabet.Add(c.ToString());

            OnPropertyChanged("Alphabet");
        }

        private void SetRotorSettings()
        {
            List<string> walzenlage = new List<string> { Rotors[SelectedRotor1Index], Rotors[SelectedRotor2Index], Rotors[SelectedRotor3Index] };
            string ringstellungs = Alphabet[Rotor1RingStellung] + Alphabet[Rotor2RingStellung] + Alphabet[Rotor3RingStellung];
            string positions = Alphabet[Rotor1Position] + Alphabet[Rotor2Position] + Alphabet[Rotor3Position];

            SetWalzenlage(walzenlage);
            SetRingStellungs(ringstellungs);
            SetPositions(positions);
        }

        private void CodeDecodeMessage()
        {
            OriginalMessage = OriginalMessage.ToUpper();
            OnPropertyChanged(nameof(OriginalMessage));

            CodedDecodedMessage = string.Empty;

            foreach (char c in OriginalMessage.ToCharArray())
                CodedDecodedMessage += EncodeCharacter(c);

            OnPropertyChanged(nameof(CodedDecodedMessage));
        }

        private void InitCommands()
        {
            SetRotorSettingsCommand = new GeneralNoParameterCommand(SetRotorSettings);
            CodeDecodeMessageCommand = new GeneralNoParameterCommand(CodeDecodeMessage);
        }
    }
}

using EnigmaComponents.Exceptions;
using EnigmaComponents.Interfaces;
using System;

namespace EnigmaComponents.Classes
{
    public abstract class Rotor
    {
        public Rotor(string rotorName, int? rotorNumber)
        {
            _rotorName = rotorName;

            if (rotorNumber != null)
                RotorNumber = (int)rotorNumber;

            PreviousRotorIsAtTurnoverPosition = false;
        }
        private string _rotorName;

#region Properties

		/// <summary>
		/// The order of the columns in Wiring is right,left:
		/// </summary>
		public abstract char[,] Wiring { get; }
        public abstract char Notch { get; }
        public abstract char Turnover { get; }

        public int RingStellung { get; protected set; }
        public int RotorNumber { get; private set; }
        public string RotorName { get { return _rotorName; } }
        public int Position { get; set; }
        public int MaxIndex
        {
            get
            {
                return Wiring.GetLength(0) - 1;
            }
        }
        public bool PreviousRotorIsAtTurnoverPosition { get; private set; }

#endregion

#region Events

		public virtual event EventHandler RotorTurnoverEvent;
        protected void OnRotorTurnoverEventTriggered(object sender, RotorTurnoverEventArgs e)
        {
            RotorTurnoverEvent?.Invoke(this, e);
        }
        public void RotorTurnoverEventHandler(object sender, EventArgs e)
        {
            PreviousRotorIsAtTurnoverPosition = true;
        }

        public virtual event EventHandler DoubleStepMiddleRotorEvent;
        protected void OnDoubleStepMiddleRotorEventTriggered(object sender, RotorTurnoverEventArgs e)
        {
            DoubleStepMiddleRotorEvent?.Invoke(this, e);
        }
        public void DoubleStepMiddleRotorEventHandler(object sender, EventArgs e)
        {
            // For double stepping to occure, we need to fake that Rotor 1 is at turnover position:
            PreviousRotorIsAtTurnoverPosition = true;
            Step();
        }

#endregion

		private int GetIndexInputChar(char inChar, bool fromRight = true)
        {
            int ind = -1;
            // The order of the columns in Wiring is right,left:
            int col = fromRight ? 0 : 1;

            for(int i=0; i<Wiring.GetLength(0); i++)
            {
                if(Wiring[i,col] == inChar)
                {
                    ind = i;
                    break;
                }
            }

            return ind;
        }

        public int GetIndexOutputPosition(int outContact, int ringStellung, int position)
        {
            int outputPosition = outContact + ringStellung - position;
            
            if (outputPosition < 0)
                outputPosition = (MaxIndex + 1) + outputPosition;
            else if(outputPosition > MaxIndex)
            {
                int diffContactMaxIndex = MaxIndex - outContact;

                outputPosition = ringStellung - diffContactMaxIndex - 1 - position;
            }

            return outputPosition;
        }

        public int GetIndexInputContact(char inputPosition, int maxIndex)
        {
            int indexInputPosition = GetIndexInputChar(inputPosition);

			if (indexInputPosition == -1)
				throw new ScramblingException(_rotorName, Enums.ScramblingExceptionCause.InvalidCharacterInput, inputPosition.ToString());

			int indexInputContact = indexInputPosition + (Position - RingStellung);    // Innkontakt:	Innposisjon - (A+A + (Ringstellung - A))

            if (indexInputContact < 0)
                indexInputContact = (maxIndex + 1) + indexInputContact;
            else if (indexInputContact > MaxIndex)
                indexInputContact = indexInputContact - (MaxIndex + 1);

            return indexInputContact;
        }

        public void SetRingStellung(char stellung)
        {
            int indStellung = GetIndexInputChar(stellung);
            RingStellung = indStellung;
        }

        public void SetPosition(char position)
        {
            int indChar = GetIndexInputChar(position);
            Position = indChar;

            if (position == Turnover)
            {
                RotorTurnoverEventArgs args = new RotorTurnoverEventArgs(_rotorName);
                OnRotorTurnoverEventTriggered(this, args);
            }
        }

		/// <summary>
		/// When the operator enters a letter, the signal passes from right to left, 
		///  as seen from the operator's perspective.
		/// </summary>
		/// <param name="inChar"></param>
		/// <returns></returns>
        public char ScrambleFromRight(char inChar)
        {
            int indexInputContact = GetIndexInputContact(inChar, MaxIndex);

            char outputContact = Wiring[indexInputContact,1];
            int indexOutputContact = GetIndexInputChar(outputContact);

            int indexOutputPosition = GetIndexOutputPosition(indexOutputContact, RingStellung, Position);
            char outputPosition = Wiring[indexOutputPosition, 0];
            
            return outputPosition;
        }

        public char ScrambleFromLeft(char leftChar)
        {
            bool fromRight = false;
            int indexLeftContact = GetIndexInputContact(leftChar, MaxIndex);

            char leftContact = Wiring[indexLeftContact, 0];
            int indexRightContact = GetIndexInputChar(leftContact, fromRight);

            int indexOutputPosition = indexRightContact + RingStellung - Position;

            if (indexOutputPosition < 0)
                indexOutputPosition = (MaxIndex + 1) + indexOutputPosition;
            else if (indexOutputPosition > MaxIndex)
            {
                int diffContactMaxIndex = indexOutputPosition - MaxIndex;

                indexOutputPosition = diffContactMaxIndex - 1;
            }

            char outputPosition = Wiring[indexOutputPosition, 0];

            return outputPosition;
        }

        public virtual void Step()
        {
            RotorTurnoverEventArgs args = new RotorTurnoverEventArgs(_rotorName);
            int indexTurnover = GetIndexInputChar(Turnover);

			// one-based numbering, starting from the right, seen from the operator's perspective.
			// RotorNumber == 1, the right-most Rotor. This rotor will step each time.
			if (RotorNumber == 1 || PreviousRotorIsAtTurnoverPosition)
                Position++;

			// To emulate a wheel.... After Z comes A:
            if (Position > MaxIndex)
                Position = 0;

            if (Position == indexTurnover)
                OnRotorTurnoverEventTriggered(this, args);

            // We need to emulate the double stepping anomaly that the physical Enigma machines suffered from.
            // When the third rotor steps, the middle rotor will also step.
			// In the case of Rotor 3, "Previous rotor" means the middle rotor.
            if (RotorNumber == 3 && PreviousRotorIsAtTurnoverPosition)
                OnDoubleStepMiddleRotorEventTriggered(this, args);

            PreviousRotorIsAtTurnoverPosition = false;
        }
    }
}

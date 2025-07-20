using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaComponents
{
    public class RotorTurnoverEventArgs : EventArgs
    {
        public RotorTurnoverEventArgs(string rotorName)
        {
            RotorName = rotorName;
        }

        public string RotorName { get; private set; }
    }
}

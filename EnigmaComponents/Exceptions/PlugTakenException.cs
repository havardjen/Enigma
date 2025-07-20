
using System;

namespace EnigmaComponents.Exceptions
{
    public class PlugTakenException : Exception
    {
        public PlugTakenException()
        {}

        public int FromStatus { get; private set; }
        public int ToStatus { get; private set; }
    }
}

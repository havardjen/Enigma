using EnigmaComponents.Classes;
using EnigmaComponents.Enums;
using System;

namespace EnigmaComponents.Exceptions
{
    public class ScramblingException : Exception
    {
        public ScramblingException(string sourceUnit, ScramblingExceptionCause cause, string invalidChar = null)
        {
            SourceUnit = sourceUnit;
            Cause = cause;
            InvalidCharacter = invalidChar;
        }
        public string SourceUnit { get; private set; }
        public ScramblingExceptionCause Cause { get; private set; }
        public string InvalidCharacter { get; private set; }
    }
}

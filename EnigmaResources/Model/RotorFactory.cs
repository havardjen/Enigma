using EnigmaComponents.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaResources.Model
{
    public static class RotorFactory
    {
        public static Rotor Create(string rotorType, int rotorNumber)
        {
            return rotorType switch
            {
                "RotorI" => new RotorI(rotorNumber),
                "RotorIII" => new RotorIII(rotorNumber),
                // legg til flere etter behov
                _ => throw new ArgumentException("Unknown rotor type")
            };
        }
    }
}

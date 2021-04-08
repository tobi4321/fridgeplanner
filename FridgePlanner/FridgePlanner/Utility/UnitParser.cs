using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public static class UnitParser
    {
        public static double KilogrammToGramm(double kg)
        {
            double g = kg * 1000;
            return g;
        }
        public static double GrammToKilogramm(double g)
        {
            double kg = g / 1000;
            return kg;
        }
        public static double LiterToMililiter(double l)
        {
            double ml = l * 1000;
            return ml;
        }
        public static double MililiterToLiter(double ml)
        {
            double l = ml / 1000;
            return l;
        }
        public static Object ParseToUnit(string targetUnit, double input)
        {
            Object Output = 0.0;

            switch (targetUnit) {
                case "Kg":
                    Output = GrammToKilogramm(input);
                    break;
                case "g":
                    Output = KilogrammToGramm(input);
                    break;
                case "L":
                    Output = MililiterToLiter(input);
                    break;
                case "Ml":
                    Output = LiterToMililiter(input);
                    break;
                default:
                    Output = null;
                    break;
            }
            return Output;
        }

    }
}

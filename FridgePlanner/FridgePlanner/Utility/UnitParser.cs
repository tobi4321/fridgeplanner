using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public static class UnitParser
    {
        public static double KgToG(double kg)
        {
            double g = kg * 1000;
            return g;
        }
        public static double GToKg(double g)
        {
            double kg = g / 1000;
            return kg;
        }
        public static double LToMl(double l)
        {
            double ml = l * 1000;
            return ml;
        }
        public static double MlToL(double ml)
        {
            double l = ml / 1000;
            return l;
        }
        public static Object ParseToUnit(string Unit, double Input)
        {
            Object Output = 0.0;

            switch (Unit) {
                case "Kg":
                    Output = GToKg(Input);
                    break;
                case "g":
                    Output = KgToG(Input);
                    break;
                case "L":
                    Output = MlToL(Input);
                    break;
                case "Ml":
                    Output = LToMl(Input);
                    break;
                default:
                    Output = null;
                    break;
            }
            return Output;
        }

    }
}

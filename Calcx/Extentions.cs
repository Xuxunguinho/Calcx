/**********************************************
 *  This software was developed by :
 *  
 *  Júlio José de Andrade Reis
 *  Email: julioreisdev@outlook.com 
 * 
 * OpenSource
 **********************************************/

using System;
using System.Linq;

namespace Calcx
{
    internal static class Extentions
    {
        public static char[] Operators = Interpreter.SuportedOperators;
        public static double[] ExtractNumbers(this string source)
        {
            try
            {
                var nums = source.Split(Operators.ToArray());
                var values = new double[nums.Length];
                for (var i = 0; i < values.Length; i++)
                {
                    double.TryParse(nums[i], out var st);
                    values[i] = st;
                }
                return values;
            }
            catch (Exception)
            {
                return new double[] { };
            }
        }
        public static double[] ExtractNumbers(this string source, char[] op)
        {
            try
            {
                var nums = source.Split(op);
                var values = new double[nums.Length];
                for (var i = 0; i < values.Length; i++)
                {
                    double.TryParse(nums[i], out var st);
                    values[i] = st;
                }
                return values;
            }
            catch (Exception)
            {
                return new double[] { };
            }
        }

       public static char[] ExtractOperators(this string source)
        {
            var list = source.ToCharArray();
            return list.Where(Operators.Contains).ToArray();
        }

        public  static char[] ExtractOperators(this string source, char[] op)
        {
            var list = source.ToCharArray();
            return list.Where(op.Contains).ToArray();
        }
      
    }
}
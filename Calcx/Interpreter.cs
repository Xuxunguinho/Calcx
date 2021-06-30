/**********************************************
 *  This software was developed by:
 *  
 *  Júlio José de Andrade Reis
 *  Email: julioreisdev@outlook.com 
 * 
 * OpenSource
 **********************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Calcx
{
    public class Interpreter
    {
        public static char[] SuportedOperators = { '+', '*', 'x', '-', '%', '/', '÷', '\\', '^' };
        public double Compile(string sourceScript)
        {
            var st = new Stopwatch();
            st.Start();
            sourceScript += " + 0";
            return Interpret(TakeCareParenteses(sourceScript));
        }
        private  double Interpret(string sources)
        {
            var m = new Helper();
            var tempOperators = new List<char>();
            var tempNumbers = new List<double>();
            var usedIndexList = new List<int>();
            var extractedNumbers = sources.ExtractNumbers();
            var extractedOperators = sources.ExtractOperators();

            if (extractedOperators.Length < 1) return double.Parse(sources);

            for (var i = 0; i < extractedOperators.Length; i++)
            {
                m.RightCount = extractedNumbers.Length - (i + 1);
                m.LeftCount = extractedNumbers.Length - m.RightCount;

                double valueM = 1;
                switch (extractedOperators[i])
                {
                    case '*':

                        if (usedIndexList.Contains(i))
                        {
                            m.RightNumber = extractedNumbers[i + 1];
                            valueM = tempNumbers.Last() * m.RightNumber;
                            tempNumbers.RemoveAt(tempNumbers.Count - 1);
                            tempNumbers.Add(valueM);
                            usedIndexList.Add(i + 1);
                        }
                        else
                        {
                            m.LeftNumber = extractedNumbers[i];
                            m.RightNumber = extractedNumbers[i + 1];
                            usedIndexList.Add(i + 1);
                            m.Mult();
                            valueM = m.Result;
                            tempNumbers.Add(valueM);
                        }
                        break;
                    case 'x':

                        if (usedIndexList.Contains(i))
                        {
                            m.RightNumber = extractedNumbers[i + 1];
                            valueM = tempNumbers.Last() * m.RightNumber;
                            tempNumbers.RemoveAt(tempNumbers.Count - 1);
                            tempNumbers.Add(valueM);
                            usedIndexList.Add(i + 1);
                        }
                        else
                        {
                            m.LeftNumber = extractedNumbers[i];
                            m.RightNumber = extractedNumbers[i + 1];
                            usedIndexList.Add(i + 1);
                            m.Mult();
                            valueM = m.Result;
                            tempNumbers.Add(valueM);
                        }
                        break;
                    case '/':
                        if (usedIndexList.Contains(i))
                        {
                            m.RightNumber = extractedNumbers[i + 1];
                            valueM = tempNumbers.Last() / m.RightNumber;
                            tempNumbers.RemoveAt(tempNumbers.Count - 1);
                            tempNumbers.Add(valueM);
                            usedIndexList.Add(i + 1);
                        }
                        else
                        {
                            m.LeftNumber = extractedNumbers[i];
                            m.RightNumber = extractedNumbers[i + 1];
                            usedIndexList.Add(i + 1);

                            valueM = m.Div();
                            tempNumbers.Add(valueM);
                        }
                        break;
                    case '÷':
                        if (usedIndexList.Contains(i))
                        {
                            m.RightNumber = extractedNumbers[i + 1];
                            valueM = tempNumbers.Last() / m.RightNumber;
                            tempNumbers.RemoveAt(tempNumbers.Count - 1);
                            tempNumbers.Add(valueM);
                            usedIndexList.Add(i + 1);
                        }
                        else
                        {
                            m.LeftNumber = extractedNumbers[i];
                            m.RightNumber = extractedNumbers[i + 1];
                            usedIndexList.Add(i + 1);

                            valueM = m.Div();
                            tempNumbers.Add(valueM);
                        }
                        break;
                    case '\\':
                        if (usedIndexList.Contains(i))
                        {
                            m.RightNumber = extractedNumbers[i + 1];
                            valueM = tempNumbers.Last() / m.RightNumber;
                            tempNumbers.RemoveAt(tempNumbers.Count - 1);
                            tempNumbers.Add((int) valueM);
                            usedIndexList.Add(i + 1);
                        }
                        else
                        {
                            m.LeftNumber = extractedNumbers[i];
                            m.RightNumber = extractedNumbers[i + 1];
                            usedIndexList.Add(i + 1);
                            valueM = m.IntDiv();
                            tempNumbers.Add((int) valueM);
                        }
                        break;
                    case '%':
                        if (usedIndexList.Contains(i))
                        {
                            m.RightNumber = extractedNumbers[i + 1];
                            valueM = tempNumbers.Last() % m.RightNumber;
                            tempNumbers.RemoveAt(tempNumbers.Count - 1);
                            tempNumbers.Add(valueM);
                            usedIndexList.Add(i + 1);
                        }
                        else
                        {
                            m.LeftNumber = extractedNumbers[i];
                            m.RightNumber = extractedNumbers[i + 1];
                            usedIndexList.Add(i + 1);
                            valueM = m.RestDiv();
                            tempNumbers.Add(valueM);
                        }
                        break;
                    default:

                        if (!usedIndexList.Contains(i))
                        {
                            m.LeftNumber = extractedNumbers[i];
                            tempNumbers.Add(m.LeftNumber);
                        }
                        tempOperators.Add(extractedOperators[i]);
                        break;
                }
            }
            double value = 0;
            if (tempNumbers.Count < 1) return value;

            value = tempNumbers[0];
            for (var i = 1; i < tempNumbers.Count; i++)
            {
                switch (tempOperators[i - 1])
                {
                    case '+':
                        value += tempNumbers[i];
                        break;
                    case '-':
                        value -= tempNumbers[i];
                        break;
                    default:
                        value += 0;
                        break;
                }
            }
            
            var ts = value;
            return ts;
        }
        private string TakeCareParenteses(string source)
        {
            var strs = string.Empty;
            var blocked = false;
            var idx = 0;
            if (!(source.Contains("(") || source.Contains(")"))) return TakeCareExpoents(source);
            for (var i = 0; i < source.Length; i++)
            {
                switch (source[i])
                {
                    case '(':
                        blocked = true;
                        idx = i;
                        break;
                    case ')':
                        var idx1 = i;
                        var str = source.Substring(idx + 1, idx1 - idx - 1);

                        str += "+0";

                        var num = Interpret(str);

                        strs += (num);
                        blocked = false;
                        break;
                    default:
                        if (!blocked)
                            strs += source[i].ToString();
                        break;
                }
            }
            var st = TakeCareExpoents(strs);
            return st;
        }

        private string TakeCareExpoents(string source)
        {
            if (!source.Contains("^")) return source;

            var oprs = source.ExtractOperators(new[] {'^', '+', '*', 'x', '-', '%', '/', '\\'});
            var context = new List<string>();
            var ns = source.ExtractNumbers(oprs);

            var m = new Helper();
            var usedsId = new List<int>();


            for (var i = 0; i < oprs.Length; i++)
            {
                m.RightCount = source.Length - (i + 1);
                m.LeftCount = source.Length - m.RightCount;

                switch (oprs[i])
                {
                    case '^':

                        if (usedsId.Contains(i))
                        {
                            m.RightNumber = ns[i + 1];
                            var ct = Math.Pow(double.Parse(context.Last()), m.RightNumber);
                            context.RemoveAt(context.Count - 1);
                            context.Add(ct.ToString());
                            usedsId.Add(i + 1);
                        }
                        else
                        {
                            var ct = Math.Pow(ns[i], ns[i + 1]);
                            context.Add(ct.ToString());
                            usedsId.Add(i);
                            usedsId.Add(i + 1);
                        }
                        break;
                    default:
                        if (!usedsId.Contains(i))
                        {
                            context.Add(ns[i].ToString());

                            usedsId.Add(i);
                        }
                        context.Add(oprs[i].ToString());
                        break;
                }
            }

            return context.Aggregate(string.Empty, (current, n) => current + (n)) + "0";
        }
    }
}
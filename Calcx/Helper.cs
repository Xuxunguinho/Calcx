/**********************************************
 *  This software was developed by :
 *  
 *  Júlio José de Andrade Reis
 *  Email: julioreisdev@outlook.com 
 * 
 * OpenSource
 **********************************************/

using System;

namespace Calcx
{
    internal class Helper
    {
        public int RightCount { get; set; }
        public int LeftCount { get; set; }
        public double LeftNumber { get; set; }
        public double RightNumber { get; set; }
        public double Result { get; private set; }
        public double Temp { get; set; }
        public void Sum()
        {
            Result = LeftNumber + RightNumber;
        }
        public double Div()
        {
            Result = LeftNumber / RightNumber;
            return Result;
        }

        public double IntDiv()
        {
            Result = Math.Abs(LeftNumber / RightNumber);
            return Result;
        }

        public double Minus()
        {
            Result = LeftNumber - RightNumber;
            return Result;
        }

        public double Mult()
        {
            Result = LeftNumber * RightNumber;
            return Result;
        }

        public double RestDiv()
        {
            Result = LeftNumber % RightNumber;
            return Result;
        }
    }
}
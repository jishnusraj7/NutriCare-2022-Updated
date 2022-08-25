using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLNutrition
{
    public class Functions
    {
        public static string ProperCase(string stringInput)
        {
            StringBuilder sb = new StringBuilder();
            bool fEmptyBefore = true;
            foreach (char ch in stringInput)
            {
                char chThis = ch;
                if (Char.IsWhiteSpace(chThis))
                    fEmptyBefore = true;
                else
                {
                    if (Char.IsLetter(chThis) && fEmptyBefore)
                        chThis = Char.ToUpper(chThis);
                    else
                        chThis = Char.ToLower(chThis);
                    fEmptyBefore = false;
                }
                sb.Append(chThis);
            }
            return sb.ToString();
        }

        public static string ReplaceChar(string stringInput)
        {
            if (stringInput != null)
                return stringInput.Replace("'", "''");
            else
                return "";
        }

        public static string EncryptString(string p_sStr)
        {
            string sPwdRet = p_sStr.Trim();
            byte[] btPwd = GetCharArray(sPwdRet);
            ushort iUBArr = Convert.ToUInt16(btPwd.GetLength(0));
            sPwdRet = string.Empty;
            for (ushort iNo = 0; iNo < iUBArr; iNo++)
                sPwdRet += String.Format("{0:x2}", (btPwd[iNo] ^ 199)).PadLeft(2, '0');
            return (sPwdRet);
        }

        private static byte[] GetCharArray(string p_sStr)
        {
            Encoding ascii = Encoding.ASCII;
            char[] cStr = p_sStr.ToCharArray();
            byte[] btStr = ascii.GetBytes(cStr);
            return (btStr);
        }

        /// <summary>
        /// Convert string to integer
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>int</returns>
        public static int Convert2Int(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    int val = int.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to short.
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>short</returns>
        public static short Convert2Short(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    short val = short.Parse(expression);
                    return (val);
                }
                catch
                {
                    return ((short)(0));
                }
            }
        }

        /// <summary>
        /// Convert string to long
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>long</returns>
        public static long Convert2Long(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    long val = long.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to float
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>float</returns>
        public static float Convert2Float(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    float val = float.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to Decimal
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>Decimal</returns>
        public static decimal Convert2Decimal(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    decimal val = decimal.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }


        /// <summary>
        /// Convert string to double
        /// </summary>
        /// <param name="expression">string</param>
        /// <returns>double</returns>
        public static double Convert2Double(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    double val = double.Parse(expression);
                    val = Math.Round(val, 2);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }

        /// <summary>
        /// Convert string to byte
        /// </summary>
        /// <param name="expression">byte</param>
        /// <returns>byte</returns>
        public static byte Convert2Byte(string expression)
        {
            if (expression.Length == 0)
                return (0);
            else
            {
                try
                {
                    expression = expression.Replace(",", "");
                    byte val = byte.Parse(expression);
                    return (val);
                }
                catch
                {
                    return (0);
                }
            }
        }
    }
}

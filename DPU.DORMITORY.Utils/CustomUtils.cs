using System;
using System.Security.Cryptography;
using System.Text;

namespace DPU.DORMITORY.Utils
{
    public class CustomUtils
    {
        //private static readonly Random getrandom = new Random();
        //private static readonly object syncLock = new object();
        //public static int GetRandomNumber(int min, int max)
        //{
        //    lock (syncLock)
        //    { // synchronize
        //        return getrandom.Next(min, max);
        //    }
        //}
        public static int GetRandomPKID()
        {
            return int.Parse(DateTime.Now.ToString("HHmmss"));

        }

        public static Boolean isNumber(String _value)
        {
            //int n;
            //return int.TryParse(_value, out n);
            try
            {
                Double.Parse(_value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static String EncodeMD5(String password)
        {


            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }

        //public static String GetCellValue(ICell _cell)
        //{
        //    String returnValue = String.Empty;
        //    if (_cell != null)
        //    {
        //        switch (_cell.CellType)
        //        {
        //            case CellType.Blank:
        //                break;
        //            case CellType.Boolean:
        //                break;
        //            case CellType.Error:
        //                break;
        //            case CellType.Formula:
        //                returnValue = _cell.StringCellValue.ToString();
        //                break;
        //            case CellType.Numeric:
        //                returnValue = _cell.NumericCellValue.ToString();
        //                break;
        //            case CellType.String:
        //                returnValue = _cell.StringCellValue.ToString();
        //                break;
        //            case CellType.Unknown:
        //                break;
        //        }
        //    }
        //    return returnValue;
        //}


        public static Double GetMax(Double _value)
        {
            return (_value > 0) ? _value : 0;
        }

        public static Double GetDefaultZero(String _value)
        {
            return String.IsNullOrEmpty(_value) ? 0 : Convert.ToDouble(_value);
        }

        public static double Average(Double[] valueList)
        {
            double result = 0.0;
            foreach (double value in valueList)
            {
                result += value;
            }
            return result / valueList.Length;
        }

        public static double StandardDeviation(Double[] valueList)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 2));
        }

        public static double Sum(String[] valueList)
        {
            double result = 0.0;
            foreach (String value in valueList)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    result += isNumber(value) ? Convert.ToDouble(value) : 0;
                }
            }
            return result;
        }

        public static double ValueIfNullToZero(String value)
        {
            double result = 0.0;

            if (!String.IsNullOrEmpty(value))
            {
                result += Convert.ToDouble(value);
            }

            return result;
        }


        public static DateTime converFromDDMMYYYY(String _val)
        {
            //26/01/2015
            String[] data = _val.Split('/');
            if (data.Length == 2)
            {
                return Convert.ToDateTime(String.Format("{0}/{1}/{2}", data[0], DateTime.DaysInMonth(Convert.ToInt32(data[1]), Convert.ToInt32(data[0])), data[1]));
            }
            else
            {
                return Convert.ToDateTime(String.Format("{0}/{1}/{2}", data[1], data[0], data[2]));
            }
            

        }

        public static string getMachineKey(int argv)
        {
            int len = 128;
            if (argv > 0)
                len = argv;

            byte[] buff = new byte[len / 2];
            RNGCryptoServiceProvider rng = new
                                    RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));
            return sb.ToString();

        }
    }
}

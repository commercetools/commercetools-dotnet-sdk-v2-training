using System;
using System.Linq;

namespace Training
{
    /// <summary>
    /// This utility class for the global variables, utility functions will be used across all exercises
    /// </summary>
    public static class Settings
    {
        private static Random random = new Random();
        
        public const string DEFAULTCLIENT = "Client";//Default client name in appsettings.test.json
        public const string MACHINELEARNINGCLIENT = "MachineLearningClient"; // Machine client name
        public const string CUSTOMERID = "e65458d0-b350-4c52-984b-bbf6db188748";//Customer ID
        public const string CATEGORYKEY = "Category1-Key-123";//Category Key
        public const string PRODUCTTYPEID = "c777235b-7a62-4bee-a13e-2e010e7d037e";//Product Type ID
        public const string PRODUCTTYPEKEY = "Jacket-PT";//Product Type Key
        public const string PRODUCTVARIANTSKU = "test123";//Product variant sku
        public const string CARTID = "101ce2bb-2948-4f30-9970-2bf0d4716b74";// Cart Id
        public const string DISCOUNTCODE = "XXXXXX"; //discount code
        public const string ORDERNUMBER = "Order1846460484"; //Order Number
        
        /// <summary>
        /// Get Random string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        /// <summary>
        /// String representing a number which is greater than 0 and smaller than 1. It should start with “0.” and should not end with “0”.
        /// </summary>
        /// <returns></returns>
        public static string RandomSortOrder()
        {
            int append = 5;//hack to not have a trailing 0 which is not accepted in sphere
            return "0." + random.Next() + append;
        }

        public static int RandomInt()
        {
            return random.Next();
        }
    }
}
using System;
using System.Linq;

namespace Training
{
    /// <summary>
    /// This utility class for the global variables, utility functions will be used across all exercises
    /// </summary>
    public static class Settings
    {
        public static string ProjectKey { get; private set; } 
        private static Random random = new Random();

        public const string DEFAULTCLIENT = "Client";//Default client name in appsettings.test.json
        public const string MACHINELEARNINGCLIENT = "MachineLearningClient"; // Machine client name
        public const string CUSTOMERID = "5d5250a2-a5eb-423e-bde2-3d9f570348b9";//Customer ID
        public const string CUSTOMERKEY = "F5TLDXZICK";//Customer Key
        public const string CATEGORYKEY = "plant-seeds";//Category Key
        public const string PRODUCTTYPEID = "c777235b-7a62-4bee-a13e-2e010e7d037e";//Product Type ID
        public const string PRODUCTTYPEKEY = "plant-seed-product-type";//Product Type Key
        public const string PRODUCTKEY = "tulip-seed-product";//Product Key
        public const string PRODUCTVARIANTSKU = "XUDO7SO85V";//Product variant sku
        public const string CARTID = "c1af9744-0db8-4e6b-908f-4409f2d5ffa5";// Cart Id
        public const string DISCOUNTCODE = "TWENTY"; //discount code
        public const string ORDERNUMBER = "Order317372326"; //Order Number

        /// <summary>
        /// Get Random string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length = 10)
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

        public static void SetCurrentProjectKey(string projectKey)
        {
            ProjectKey = projectKey;
        }
    }
}

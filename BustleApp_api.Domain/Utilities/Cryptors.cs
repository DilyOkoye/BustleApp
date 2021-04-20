using System;
using System.Security.Cryptography;
using System.Text;
namespace BustleApp_api.Domain.Utilities
{
    public class Cryptors
    {
        public static string GetSHAHashData(string strInput)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();

            //provide the string in byte format to the ComputeHash method. 
            //This method returns the SHA-512 hash code in byte array
            byte[] arrHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(strInput));

            // use a Stringbuilder to append the bytes from the array to create a SHA-512 hash code string.
            StringBuilder sbHash = new StringBuilder();

            // Loop through byte array of the hashed code and format each byte as a hexadecimal code.
            for (int i = 0; i < arrHash.Length; i++)
            {
                sbHash.Append(arrHash[i].ToString("x2"));
            }

            // Return the hexadecimal SHA-512 hash code string.
            return sbHash.ToString();
        }

    }
}

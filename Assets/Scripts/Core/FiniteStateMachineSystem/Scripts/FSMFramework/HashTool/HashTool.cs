using System;
using System.Security.Cryptography;
using System.Text;

public class HashTool
{
    /// <summary>
    /// Convert the string to hash value
    /// </summary>
    /// <param name="stringValue">the original string</param>
    /// <returns>the hash string</returns>
    public static string StringToHash(string stringValue)
    {
        try
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] byteArray = Encoding.ASCII.GetBytes(stringValue);

            byteArray = md5.ComputeHash(byteArray);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < byteArray.Length; i++)
            {
                sb.Append(byteArray[i].ToString("x2"));
            }

            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("compute md5 fail, error:" + ex.Message);
        }

    }


}

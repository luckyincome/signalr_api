using Signalr_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Signalr_API.Util
{
    public class Common
    {

        public static string generateMD5ResultOut(ResultOutModel model, string secretKey)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(model.set + model.setvalue + model.number + secretKey));
            byte[] encryptResult = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < encryptResult.Length; i++)
            {
                strBuilder.Append(encryptResult[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}

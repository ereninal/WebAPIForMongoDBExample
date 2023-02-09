using System.Security.Cryptography;
using System.Text;

namespace WebAPIForMongoDB.Core.Security
{
    public static class MD5Security
    {
        public const string NoLicense = "NoLicense";

        public const string DefaultKey = "+=5^3_^=+8=-";
        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(this string strToEncrypt, string polynomialKey = null)
        {
            string result = "";
            if (!String.IsNullOrEmpty(strToEncrypt))
            {
                try
                {
                    var objDESCrypto =
                        new TripleDESCryptoServiceProvider();
                    var objHashMD5 = new MD5CryptoServiceProvider();
                    string strTempKey = String.IsNullOrEmpty(polynomialKey) ? DefaultKey : polynomialKey;
                    byte[] byteHash = objHashMD5.ComputeHash(Encoding.ASCII.GetBytes(strTempKey));
                    objDESCrypto.Key = byteHash;
                    objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                    byte[] byteBuff = Encoding.ASCII.GetBytes(strToEncrypt);
                    result = Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                        TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                }
                catch (Exception ex)
                {
                    result = "Wrong Input. " + ex.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(this string strEncrypted, string polynomialKey = null)
        {
            string result = "";
            if (!String.IsNullOrEmpty(strEncrypted))
            {
                try
                {
                    var objDESCrypto =
                        new TripleDESCryptoServiceProvider();
                    var objHashMD5 = new MD5CryptoServiceProvider();
                    string strTempKey = String.IsNullOrEmpty(polynomialKey) ? DefaultKey : polynomialKey;
                    byte[] byteHash = objHashMD5.ComputeHash(Encoding.ASCII.GetBytes(strTempKey));
                    objDESCrypto.Key = byteHash;
                    objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                    byte[] byteBuff = Convert.FromBase64String(strEncrypted);
                    string strDecrypted = Encoding.ASCII.GetString
                    (objDESCrypto.CreateDecryptor().TransformFinalBlock
                    (byteBuff, 0, byteBuff.Length));
                    result = strDecrypted;
                }
                catch (Exception ex)
                {
                    result = "Wrong Input. " + ex.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strEncrypt"></param>
        /// <returns></returns>
        public static string GetMD5(string strEncrypt)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bytes = Encoding.Default.GetBytes(strEncrypt);
            byte[] encoded = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte current in encoded)
            {
                sb.Append(current.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Copasa.Atende.Business.Rules.dyn365
{
    /// <summary>
    /// 
    /// </summary>
   public class Criptografia
    {

        static string keyString = "E546C8DF278CD5931069B522E695D4F2";
        static List<char> invalidChars = new List<char>() { '\\', '/', '#', '+', '-', '!', '$', '@', '%', '&', '^' };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            var decryptedValue = "";
            var characterException = "";

            try
            {
                if (cipherText.IndexOf("=__") > 0)
                {
                    characterException = cipherText.Substring(cipherText.IndexOf("=__") + 3);
                    cipherText = cipherText.Substring(0, cipherText.LastIndexOf("=") + 1);
                }

                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[16];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
                var key = Encoding.UTF8.GetBytes(keyString);

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(characterException))
                        {
                            var sb = new StringBuilder(result);
                            foreach (var item in characterException.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var position = int.Parse(Regex.Match(item, @"\d+").Value);
                                var charToImcrement = item.Replace(position.ToString(), "");
                                sb.Insert(position, charToImcrement[0]);
                            }
                            result = sb.ToString();
                        }

                        return result;
                    }
                }
            }
            catch
            {
            }

            return decryptedValue;
        }

    }
}

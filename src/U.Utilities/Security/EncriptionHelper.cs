using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace U.Utilities.Security
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public class EncriptionHelper
    {
        /// <summary>
        /// 生成随机字符串（纯数字）
        /// </summary>
        /// <param name="size">位数</param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int size)
        {
            string characterSet = "0123456789";
            Random random = new Random();

            string key = new string(
                    Enumerable.Repeat(characterSet, size)
                        .Select(set => set[random.Next(set.Length)])
                        .ToArray());

            return key;
        }

        /// <summary>
        /// 生成简单加密key
        /// </summary>
        /// <param name="size">位数</param>
        /// <returns></returns>
        public static string GenerateKey(int size)
        {
            string characterSet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();

            string key = new string(
                    Enumerable.Repeat(characterSet, size)
                        .Select(set => set[random.Next(set.Length)])
                        .ToArray());

            return key;
        }

        /// <summary>
        /// 创建salt key
        /// </summary>
        /// <param name="size">key siz</param>
        /// <returns></returns>
        public static string CreateSaltKey(int size)
        {
            //生成cryptographic随机数
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            //Return a Base64 string
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 通过 saltKey 加密提供的密码（一个不可反序列化的密文）
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="saltkey">salt key</param>
        /// <param name="passwordFormat">密码格式（hash algorithm）</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("未被 hash 的名称", "hashName");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// 返回一个MD5加密后的字符串
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string MD5(string text)
        {
            byte[] b = Encoding.UTF8.GetBytes(text);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }

            return ret;
        }

        #region Base64 Encypt / Decrypt
        #region Simple
        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="plainText">加密的文本</param>
        /// <param name="encryptionPrivateKey">加密的私有key</param>
        /// <returns>已加密的文本</returns>
        public static string EncryptText(string plainText, string encryptionPrivateKey)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;


            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="cipherText">解密的文本</param>
        /// <param name="encryptionPrivateKey">解密的私有key</param>
        /// <returns>已解密的文本</returns>
        public static string DecryptText(string cipherText, string encryptionPrivateKey)
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }
        #endregion
        /// <summary>
        /// Encrypts the plainText input using the given Key.
        /// A 128 bit random salt will be generated and prepended to the ciphertext before it is base64 encoded.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="key">The plain text encryption key.</param>
        /// <returns>The salt and the ciphertext, Base64 encoded for convenience.</returns>
        public static string Encrypt(string plainText, string key, int saltSize = 8)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            // Derive a new Salt and IV from the Key
            using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltSize))
            {
                var saltBytes = keyDerivationFunction.Salt;
                var keyBytes = keyDerivationFunction.GetBytes(32);
                var ivBytes = keyDerivationFunction.GetBytes(16);

                // Create an encryptor to perform the stream transform.
                // Create the streams used for encryption.
                using (var aesManaged = new AesManaged())
                using (var encryptor = aesManaged.CreateEncryptor(keyBytes, ivBytes))
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        // Send the data through the StreamWriter, through the CryptoStream, to the underlying MemoryStream
                        streamWriter.Write(plainText);
                    }

                    // Return the encrypted bytes from the memory stream, in Base64 form so we can send it right to a database (if we want).
                    var cipherTextBytes = memoryStream.ToArray();
                    Array.Resize(ref saltBytes, saltBytes.Length + cipherTextBytes.Length);
                    Array.Copy(cipherTextBytes, 0, saltBytes, saltSize, cipherTextBytes.Length);

                    return Convert.ToBase64String(saltBytes);
                }
            }
        }

        /// <summary>
        /// Decrypts the ciphertext using the Key.
        /// </summary>
        /// <param name="ciphertext">The ciphertext to decrypt.</param>
        /// <param name="key">The plain text encryption key.</param>
        /// <returns>The decrypted text.</returns>
        public static string Decrypt(string ciphertext, string key, int saltSize = 8)
        {
            if (string.IsNullOrEmpty(ciphertext))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            // Extract the salt from our ciphertext
            var allTheBytes = Convert.FromBase64String(ciphertext);
            var saltBytes = allTheBytes.Take(saltSize).ToArray();
            var ciphertextBytes = allTheBytes.Skip(saltSize).Take(allTheBytes.Length - saltSize).ToArray();

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltBytes))
            {
                // Derive the previous IV from the Key and Salt
                var keyBytes = keyDerivationFunction.GetBytes(32);
                var ivBytes = keyDerivationFunction.GetBytes(16);

                // Create a decrytor to perform the stream transform.
                // Create the streams used for decryption.
                // The default Cipher Mode is CBC and the Padding is PKCS7 which are both good
                using (var aesManaged = new AesManaged())
                using (var decryptor = aesManaged.CreateDecryptor(keyBytes, ivBytes))
                using (var memoryStream = new MemoryStream(ciphertextBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    // Return the decrypted bytes from the decrypting stream.
                    return streamReader.ReadToEnd();
                }
            }
        }
        #endregion

        ///
        /// 采用SHA-1算法加密字符串
        ///
        /// 要加密的字符串
        /// 返回加密后的字符串
        public static string EncryptSHA1(string sourceStr)
        {
            byte[] strRes = Encoding.Default.GetBytes(sourceStr);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            strRes = iSHA.ComputeHash(strRes);
            StringBuilder enText = new StringBuilder();
            foreach (byte iByte in strRes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }
            return enText.ToString();
        }

        #region Utilities

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion
    }
}

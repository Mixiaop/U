using System;
using System.Security.Cryptography.X509Certificates;
namespace U.Utilities.IIS
{
    public class BaseService
    {
        protected byte[] HexString2Bytes(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return bytes;
        }

        protected byte[] GetCertificateHash(string hash)
        {
            byte[] hashBytes = null;

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);	//个人存储区
            store.Open(OpenFlags.ReadOnly);

            foreach (var certificate in store.Certificates)
            {
                if (string.Equals(certificate.GetCertHashString(), hash, StringComparison.CurrentCultureIgnoreCase))
                {
                    hashBytes = certificate.GetCertHash();
                    break;
                }
            }
            store.Close();

            return hashBytes;
        }
    }
}

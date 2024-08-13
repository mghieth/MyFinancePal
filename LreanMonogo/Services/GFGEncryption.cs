using System.Security.Cryptography;
using System.Text;

namespace MyFinancePal.Services
{

    public interface IGFGEncryption
    {
        string encodeString( string data);
        string decodeString(String data);
    }
    public class GFGEncryption : IGFGEncryption
    {
        private readonly string  publicKey = "GEEK1234";
        private readonly string privateKey = "PKEY4321";

        public  string encodeString( string data)
        {
            string answer = "";
            byte[] privateKeyBytes = { };
            privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
            byte[] publicKeyBytes = { };
            publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);
            byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(data);
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream,
                provider.CreateEncryptor(publicKeyBytes, privateKeyBytes),
                CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                answer = Convert.ToBase64String(memoryStream.ToArray());
            }
            return answer;
        }

        public  string decodeString(String data)
        {
            string answer = "";     
            byte[] privateKeyBytes = { };
            privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
            byte[] publicKeyBytes = { };
            publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);
            byte[] inputByteArray = new byte[data.Replace(" ", "+").Length];
            inputByteArray = Convert.FromBase64String(data.Replace(" ", "+"));
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream,
                provider.CreateDecryptor(publicKeyBytes, privateKeyBytes),
                CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                answer = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return answer;
        }
    }
}

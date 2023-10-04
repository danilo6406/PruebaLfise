using System.Security.Cryptography;
using System.Text;

namespace EasySales.Server.Models
{
    public class EncryptationRepository : IEncryptationRepository
    {
        private const string EncryptionKey = "b667567c76a3370361656498314a84e7"; // Replace with your own encryption key

        public string Encrypt(string data)
        {
            byte[] encryptedBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = new byte[16]; // Initialization Vector (IV) should be randomly generated for each encryption

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
            }

            string encryptedData = Convert.ToBase64String(encryptedBytes);

            return encryptedData;
        }

        public string Decrypt(string encryptedData)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            byte[] decryptedBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = new byte[16]; // Initialization Vector (IV) should be the same as the one used during encryption

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            }

            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);

            return decryptedData;
        }

    }
}

namespace EasySales.Client.Services
{
    public interface IEncryptionService
    {
        Task<string> Encrypt(string data);
        Task<string> Decrypt(string encryptedData);
    }
}

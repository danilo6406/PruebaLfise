namespace EasySales.Server.Models
{
    public interface IEncryptationRepository
    {
        string Decrypt(string encryptedData);
        string Encrypt(string data);
    }
}
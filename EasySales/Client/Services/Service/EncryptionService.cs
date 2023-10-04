namespace EasySales.Client.Services
{
    public class EncryptionService :IEncryptionService
    {
        private readonly HttpClient httpClient;

        public EncryptionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> Encrypt(string data)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/encryptation/encriptar/{data}");
                response.EnsureSuccessStatusCode();
                string dataEncriptada = await response.Content.ReadAsStringAsync();
                return dataEncriptada;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> Decrypt(string encryptedData)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/encryptation/desencriptar/{encryptedData}");
                response.EnsureSuccessStatusCode();
                string dataDesencriptada = await response.Content.ReadAsStringAsync();
                return dataDesencriptada;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

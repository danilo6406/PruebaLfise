using EasySales.Shared;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class FilesaveService : IFilesaveService
    {
        private readonly HttpClient httpClient;

        public FilesaveService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<UploadResult> PostFile(MultipartFormDataContent formDataContent)
        {
            try
            {
                var response = await httpClient.PostAsync("/api/Filesave", formDataContent);
                var newUploadResults = await response.Content.ReadFromJsonAsync<UploadResult>();
                return newUploadResults;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

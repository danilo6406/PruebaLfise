using EasySales.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Client.Services
{
    public interface IFilesaveService
    {
        Task<UploadResult> PostFile([FromForm] MultipartFormDataContent formDataContent);
    }
}

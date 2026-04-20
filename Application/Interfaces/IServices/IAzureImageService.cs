namespace Application.Interfaces.IServices
{
    public interface IAzureImageService
    {
        Task<string> UploadImageAsync(Stream file, string folder, string contentType);
        Task<bool> DeleteAsync(string urlImage);
    }
}

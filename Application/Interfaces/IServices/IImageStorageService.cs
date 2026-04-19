namespace Application.Interfaces.IServices
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(Stream file, string folder, string contentType);
        Task<bool> DeleteAasync(string urlImage);
    }
}

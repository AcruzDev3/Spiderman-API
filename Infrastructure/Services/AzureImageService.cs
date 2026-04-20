using Application.Interfaces.IServices;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.Services
{
    public class AzureImageService : IAzureImageService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly int _maxWidth;
        private readonly int _maxHeight;
        public AzureImageService(IConfiguration configuration) {
            string connectionString = configuration.GetConnectionString("AzureStorage");
            string containerName = configuration["AzureStorage:ContainerName"];

            this._containerClient = new BlobContainerClient(connectionString, containerName);
            this._containerClient.CreateIfNotExists(PublicAccessType.Blob);

            this._maxWidth = 1080;
            this._maxHeight = 1080;
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType) {
            
            Stream optimizedStream = this.ConvertToWebP(fileStream);

            string webpFileName = Path.ChangeExtension(fileName, ".webp");

            BlobClient blobClient = this._containerClient.GetBlobClient(webpFileName);
            await blobClient.DeleteIfExistsAsync();

            await blobClient.UploadAsync(
                optimizedStream,
                new BlobUploadOptions {
                    HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
                }
            );
            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteAasync(string urlImage) {
            if (String.IsNullOrEmpty(urlImage))
                return false;

            Uri uri = new Uri(urlImage);
            string blobName = Path.GetFileName(uri.LocalPath);

            BlobClient blobClient = this._containerClient.GetBlobClient(blobName);

            Response<bool> result = await blobClient.DeleteIfExistsAsync();
            return result.Value;
        }

        private Stream ConvertToWebP(Stream inputStream) {
            inputStream.Position = 0;

            Image image = Image.Load(inputStream);

            Image resizeImage = this.ResizeImage(image, this._maxWidth, this._maxHeight);

            MemoryStream outputStream = new MemoryStream();

            resizeImage.SaveAsWebp(
                outputStream,
                new WebpEncoder { Quality = 75 }
            );

            outputStream.Position = 0;
            return outputStream;
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight) {
            if (image.Width <= maxWidth && image.Height <= maxHeight)
                return image;

            double widthRatio = (double)maxWidth / image.Width;
            double heightRatio = (double)maxHeight / image.Height;

            double scale = Math.Min(widthRatio, heightRatio);

            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);

            image.Mutate(x => x.Resize(newWidth, newHeight));

            return image;
        }
    }
}

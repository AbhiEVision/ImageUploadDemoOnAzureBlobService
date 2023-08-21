using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DemoPractical.ImageDemo.Service.Abstract;

namespace DemoPractical.ImageDemo.Service.Implementation
{
	public class ImageService : IImageService
	{

		private readonly string _connectionString;

		public ImageService(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("BlobStorage");
		}

		public string UploadImageToAzure(IFormFile file)
		{
			string fileExtension = Path.GetExtension(file.FileName);

			using MemoryStream fileUploadStraem = new MemoryStream();
			file.CopyTo(fileUploadStraem);
			fileUploadStraem.Position = 0;

			BlobContainerClient client = new BlobContainerClient(_connectionString, "demo-image-store");

			var uniqueName = $"{Guid.NewGuid()}{fileExtension}";

			BlobClient blobClient = client.GetBlobClient(uniqueName);

			blobClient.Upload(fileUploadStraem, new BlobUploadOptions()
			{
				HttpHeaders = new BlobHttpHeaders()
				{
					ContentType = "image/bitmap"
				}
			},
			default);

			return uniqueName;
		}
	}
}

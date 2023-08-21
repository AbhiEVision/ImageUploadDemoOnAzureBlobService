namespace DemoPractical.ImageDemo.Service.Abstract
{
	public interface IImageService
	{
		string UploadImageToAzure(IFormFile file);
	}
}

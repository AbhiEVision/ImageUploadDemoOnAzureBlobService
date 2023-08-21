using System.ComponentModel;

namespace DemoPractical.ImageDemo.Models
{
	public class ImageModel
	{
		[DisplayName("Upload Image Here")]
		public required string FileDetails { get; set; }

		public required IFormFile File { get; set; }


	}
}

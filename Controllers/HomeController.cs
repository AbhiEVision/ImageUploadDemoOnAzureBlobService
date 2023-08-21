using DemoPractical.ImageDemo.Models;
using DemoPractical.ImageDemo.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoPractical.ImageDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IImageService _imageService;
		private readonly string _baseUrl = "https://imagestoredemoonblob.blob.core.windows.net/demo-image-store/";
		private static string currImageUrl = null;

		public HomeController(ILogger<HomeController> logger, IImageService imageService = null)
		{
			_logger = logger;
			_imageService = imageService;
		}

		public IActionResult Index()
		{
			if (currImageUrl != null)
			{
				ViewBag.ImageUrl = currImageUrl;
			}
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SavePicture(ImageModel model)
		{

			//using (Stream fileStream = new FileStream(
			//	@"C:\temp\" + model.File.FileName
			//	, FileMode.Create
			//	, FileAccess.Write))
			//{
			//	model.File.CopyTo(fileStream);
			//}

			if (model.File == null)
			{
				ModelState.AddModelError("File Not Found", "File is must be uploaded");
				return View("Index");
			}

			try
			{
				string photoName = _imageService.UploadImageToAzure(model.File);

				currImageUrl = _baseUrl + photoName;

				ViewBag.ImageUrl = currImageUrl;

			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = ex.Message;
			}


			return View("Index");
		}
	}
}
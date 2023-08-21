using DemoPractical.ImageDemo.Service.Abstract;
using DemoPractical.ImageDemo.Service.Implementation;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var storageConnectionString = builder.Configuration.GetConnectionString("BlobStorage");

builder.Services.AddAzureClients(x =>
{
	x.AddBlobServiceClient(storageConnectionString);

});

builder.Services.AddSingleton<IImageService, ImageService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

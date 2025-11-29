using ImageConverterServer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using Microsoft.AspNetCore.Mvc; 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var env = builder.Environment;
var previewPath = Path.Combine(env.ContentRootPath, "rawAssets", "Canti.jpeg");
var uploadHtmlPath = Path.Combine(env.ContentRootPath, "rawAssets", "upload.html");

var converter = new ImageConverter(env);

var logFile = Path.Combine(env.ContentRootPath, "log.txt");

var listener = new ImageConverterListener(converter, logFile);

app.MapGet("/", () => "Image Converter server is running");

app.MapGet("/upload", () =>
{
    if (!File.Exists(uploadHtmlPath))
    {
        return Results.NotFound("upload.html not found");
    }

    var html = File.ReadAllText(uploadHtmlPath);
    return Results.Content(html, "text/html");
});


//preview
app.MapGet("/preview", () =>
{
    if (!File.Exists(previewPath))
    {
        return Results.NotFound("Preview image not found.");
    }

    var stream = File.OpenRead(previewPath); 
    const string contentType = "image/jpeg";  

    return TypedResults.File(stream, contentType);
});


app.MapPost("/convert", ([FromForm] IFormFile file, [FromForm] string format) =>
{
    var (ms, contentType) = converter.Convert(file, format);
    return TypedResults.File(ms, contentType);
}).DisableAntiforgery();

app.Run();


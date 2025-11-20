using ImageConverterServer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var env = builder.Environment;
var converter = new ImageConverter(env);

app.MapGet("/", () => "Image Converter server is running");

converter.ImageConverted += (s, e) =>
{
    app.Logger.LogInformation($"Converted image {e.Name} to {e.Path} in format {e.Format} at {e.Timestamp}");
};

converter.ImageConvertionFailed += (s, e) =>
{
    app.Logger.LogError($"Convertion failed for {e.Name} to {e.Format} at {e.Timestamp} : {e.Exception}");
};

app.MapPost("/convert", (IFormFile file, string format) =>
{
    var (ms, contentType) = converter.Convert(file, format);
    return TypedResults.File(ms, contentType);
}).DisableAntiforgery();

app.Run();


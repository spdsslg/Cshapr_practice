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

var logFile = Path.Combine(env.ContentRootPath, "log.txt");

var listener = new ImageConverterListener(converter, logFile);


app.MapGet("/", () => "Image Converter server is running");

app.MapPost("/convert", (IFormFile file, string format) =>
{
    var (ms, contentType) = converter.Convert(file, format);
    return TypedResults.File(ms, contentType);
}).DisableAntiforgery();

app.Run();


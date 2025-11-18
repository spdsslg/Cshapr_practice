using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Image Converter server is running");

app.MapPost("/convert", (IFormFile file, string format) =>
{
    if (file.Length < 1)
    {
        return Results.BadRequest("File must not be empty");
    }

    IImageEncoder encoder;
    string contentType;
    format = format.ToLower();
    if (format == "jpeg")
    {
        contentType = "image/jpeg";
        encoder = new JpegEncoder();
    }
    else if (format == "png")
    {
        contentType = "image/png";
        encoder = new PngEncoder();
    }
    else if (format == "gif")
    {
        contentType = "image/gif";
        encoder = new GifEncoder();
    }
    else
    {
        return TypedResults.BadRequest("Unsupported format.");
    }

    using var input = file.OpenReadStream();
    using var image = Image.Load(input);

    var outputDir = "./convertedAssets";
    if (!Directory.Exists(outputDir))
    {
        Directory.CreateDirectory(outputDir);
    }
    var convertedName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_converted.{format}";
    var fullPath = Path.Combine(outputDir, convertedName);
    image.Save(fullPath,  encoder);

    var ms = new MemoryStream();
    image.Save(ms, encoder);
    ms.Position = 0;
    
    return TypedResults.File(ms, contentType);
}).DisableAntiforgery();


app.Run();


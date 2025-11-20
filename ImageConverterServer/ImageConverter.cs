using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;

namespace ImageConverterServer;

public class ImageConverter
{
    public event EventHandler<ImageConvertedEventArgs>? ImageConverted;
    public event EventHandler<ImageConvertionFailedEventArgs>? ImageConvertionFailed;

    public ImageConverter(IHostEnvironment hostEnvironment)
    {
        var outputDir = $"{hostEnvironment.ContentRootPath}/convertedAssets";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
    }

    public (Stream OutStream, string ContentType) Convert(IFormFile file, string format)
    {
        try
        {
            if (file.Length < 1)
            {
                throw new InvalidDataException("File must not be empty");
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
                throw new FormatException("Unsupported format.");
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
            image.Save(fullPath, encoder);

            var ms = new MemoryStream();
            image.Save(ms, encoder);
            ms.Position = 0;
            
            OnImageConverted(new ImageConvertedEventArgs(file.FileName, format, fullPath));

            return (ms, contentType);
        }
        catch (Exception ex)
        {
            OnImageConvertionFailed(new ImageConvertionFailedEventArgs(file.FileName, format, ex));
            return (null, null);
        }
    }

    protected virtual void OnImageConverted(ImageConvertedEventArgs args)=> 
        ImageConverted?.Invoke(this, args);
    protected virtual void OnImageConvertionFailed(ImageConvertionFailedEventArgs args) =>
        ImageConvertionFailed?.Invoke(this, args);
}
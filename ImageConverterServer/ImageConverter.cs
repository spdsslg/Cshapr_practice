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
    
    
}
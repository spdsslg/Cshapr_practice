namespace ImageConverterServer;

public class ImageConverterListener:IDisposable
{
    private ImageConverter converter;
    private string LogPath;
    public ImageConverterListener(ImageConverter converter, string logPath)
    {
        this.converter = converter;
        LogPath = logPath;
        this.converter.ImageConverted+= OnImageConverted;
        this.converter.ImageConvertionFailed += OnImageFailedConvertion;
    }

    private void OnImageConverted(object? s, ImageConvertedEventArgs? e)
    {
        var l = ($"Converted image {e.Name} to {e.Path} in format {e.Format} at {e.Timestamp}\n");
        File.AppendAllText(LogPath, l);
    }

    private void OnImageFailedConvertion(object? s, ImageConvertionFailedEventArgs? e)
    {
        var l = ($"Failed to convert image {e.Name} to {e.Format} at {e.Timestamp} : {e.Exception}\n");
        File.AppendAllText(LogPath, l);
    }

    public void Dispose()
    {
        this.converter.ImageConverted -= OnImageConverted;
        this.converter.ImageConvertionFailed -= OnImageFailedConvertion;
    }
}
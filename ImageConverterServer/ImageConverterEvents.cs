namespace ImageConverterServer;

public class ImageConvertedEventArgs:EventArgs
{
    public string Name { get; }
    public string Format { get; }
    public string Path { get; }
    public DateTime Timestamp { get; }

    public ImageConvertedEventArgs(string name, string format, string path)
    {
        Name = name;
        Format = format;
        Path = path;
        Timestamp = DateTime.Now;
    }
}

public class ImageConvertionFailedEventArgs : EventArgs
{
    public string Name { get; }
    public string Format { get; }
    public Exception Exception { get; }
    public DateTime Timestamp { get; }
    public ImageConvertionFailedEventArgs(string name, string format, Exception exception)
    {
        Name = name;
        Format = format;
        Exception = exception;
        Timestamp = DateTime.Now;
    }
}
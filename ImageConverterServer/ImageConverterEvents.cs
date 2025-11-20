namespace ImageConverterServer;

public class ImageConvertedEventArgs:EventArgs
{
    string Name { get; }
    string Format { get; }
    string Path { get; }
    DateTime Timestamp { get; }

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
    string Name { get; }
    string Format { get; }
    Exception Exception { get; }
    DateTime Timestamp { get; }
    public ImageConvertionFailedEventArgs(string name, string format, Exception exception)
    {
        Name = name;
        Format = format;
        Exception = exception;
        Timestamp = DateTime.Now;
    }
}
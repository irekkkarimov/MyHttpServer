namespace MyHttpServer;

public class StaticFilesHandler
{
    private string StaticFolder { get; set; }

    public StaticFilesHandler(string staticFolderPath)
    {
        if (Directory.Exists(staticFolderPath))
            StaticFolder = staticFolderPath;
        else
            Directory.CreateDirectory(staticFolderPath);
    }

    public byte[] fetchStaticFile(string fileName)
    {
        return File.Exists(StaticFolder + "/" + fileName)
            ? File.ReadAllBytes(StaticFolder + "/" + fileName)
            : Array.Empty<byte>();
    }
}
namespace Movies.Helpers;
public static class Converter
{
    public static byte[] IFormFileToByteArray(IFormFile file)
    {
        using var dataStream = new MemoryStream();
        file.CopyTo(dataStream);
        return dataStream.ToArray();
    }
}

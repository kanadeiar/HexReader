namespace HexReader.Infrastructure.Helpers;

public class BinaryFileReaderHelper : IFileReaderHelper
{
    public byte[][] ReadBinaryLinesWithOffset(string filename, long offset, long count)
    {
        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "Смещение в файле не может быть меньше ноля");
        }
        var data = new byte[count][];
        using (var fstream = new FileStream(filename, FileMode.Open))
        {
            fstream.Seek(offset * 16, SeekOrigin.Begin);
            var i = 0L;
            while (fstream.Position < fstream.Length && i++ < count)
            {
                byte[] buffer = new byte[16];
                fstream.Read(buffer, 0, buffer.Length);
                data[i - 1] = buffer;
            }
        }
        return data;
    }

    public (bool exists, long size) GetFileInfo(string filename)
    {
        if (File.Exists(filename))
        {
            using (var fstream = new FileStream(filename, FileMode.Open))
            {
                return (true, fstream.Length);
            }
        }
        throw new FileNotFoundException("Файл не существует: " + filename);
    }
}

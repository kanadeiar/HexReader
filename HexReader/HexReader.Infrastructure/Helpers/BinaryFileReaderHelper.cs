using HexReader.CoreApplication.Interfaces;

namespace HexReader.Infrastructure.Helpers;

public class BinaryFileReaderHelper : IFileReaderHelper
{
    public byte[][] ReadBinaryLinesWithOffset(string filename, int offset, int count)
    {
        var data = new byte[count][];
        using (FileStream fstream = new FileStream(filename, FileMode.Open))
        {
            if (offset < 0)
            {
                offset = 0;
            }
            fstream.Seek(offset * 16, SeekOrigin.Begin);
            var i = 0;
            while (fstream.Position < fstream.Length && i++ < count)
            {
                byte[] buffer = new byte[16];
                fstream.Read(buffer, 0, buffer.Length);
                data[i - 1] = buffer;
            }
        }
        return data;
    }
}

namespace HexReader.CoreApplication.Interfaces;

public interface IFileReaderHelper
{
    byte[][] ReadBinaryLinesWithOffset(string filename, int offset, int count);
    (bool exists, long size) GetFileInfo(string filename);
}
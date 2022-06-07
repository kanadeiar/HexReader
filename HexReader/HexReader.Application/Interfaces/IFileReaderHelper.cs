namespace HexReader.CoreApplication.Interfaces;

public interface IFileReaderHelper
{
    byte[][] ReadBinaryLinesWithOffset(string filename, long offset, long count);
    (bool exists, long size) GetFileInfo(string filename);
}
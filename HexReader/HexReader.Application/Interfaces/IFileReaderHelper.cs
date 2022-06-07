namespace HexReader.CoreApplication.Interfaces;

/// <summary>
/// Помощник работы с файлом
/// </summary>
public interface IFileReaderHelper
{
    /// <summary>
    /// Прочитать данные из файла
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <param name="offset">Смещение строк</param>
    /// <param name="count">Количество строк</param>
    /// <returns>Строки данных</returns>
    Task<byte[][]> ReadBinaryLinesWithOffsetAsync(string filename, long offset, long count);
    
    /// <summary>
    /// Получить информацию о файле
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <returns>Существует, размер файла</returns>
    (bool exists, long size) GetFileInfo(string filename);
}
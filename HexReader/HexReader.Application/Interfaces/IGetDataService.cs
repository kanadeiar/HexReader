namespace HexReader.CoreApplication.Interfaces;

/// <summary>
/// Сервис получения данных из файла
/// </summary>
public interface IGetDataService
{
    /// <summary>
    /// Получить данные из файла
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <param name="offset">Смещение</param>
    /// <returns>Данные</returns>
    Task<IEnumerable<BinaryRecord>> GetLinesDataFromFileAsync(string filename, long offset);
    
    /// <summary>
    /// Получить размер файла
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <returns>размер</returns>
    long GetFileSize(string filename);
}
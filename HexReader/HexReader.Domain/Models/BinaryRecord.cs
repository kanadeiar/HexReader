namespace HexReader.Domain.Models;

/// <summary>
/// 16 байт отбражаемых бинарных данных файла
/// </summary>
public class BinaryRecord : Base.BaseModel
{
    private int _Number;
    /// <summary>
    /// Номер строки
    /// </summary>
    public int Number
    {
        get => _Number;
        set => Set(ref _Number, value);
    }

    private string _HexCodes;
    /// <summary>
    /// Коды сивололов
    /// </summary>
    public string HexCodes
    {
        get => _HexCodes;
        set => Set(ref _HexCodes, value);
    }

    private string _Text;
    /// <summary>
    /// Символы
    /// </summary>
    public string Text
    {
        get => _Text;
        set => Set(ref _Text, value);
    }
}

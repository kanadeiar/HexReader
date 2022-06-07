namespace HexReader.Domain.Models;

/// <summary>
/// 16 байт отбражаемых бинарных данных файла
/// </summary>
public class BinaryRecord : Base.BaseModel
{
    private long _Number;
    /// <summary>
    /// Номер строки
    /// </summary>
    public long Number
    {
        get => _Number;
        set => Set(ref _Number, value);
    }

    private int[] _HexCodes;
    /// <summary>
    /// Коды символов 16 байт
    /// </summary>
    public int[] HexCodes
    {
        get => _HexCodes;
        set => Set(ref _HexCodes, value);
    }

    private string _Dump;
    /// <summary>
    /// Символы
    /// </summary>
    public string Dump
    {
        get => _Dump;
        set => Set(ref _Dump, value);
    }
}

namespace HexReader.CoreDomain.Models;

/// <summary>
/// 16 байт отбражаемых бинарных данных файла
/// </summary>
public class BinaryRecord : BaseModel
{
    private string _Number;
    /// <summary>
    /// Номер строки
    /// </summary>
    public string Number
    {
        get => _Number;
        set => Set(ref _Number, value);
    }

    private string[] _HexCodes;
    /// <summary>
    /// Коды символов
    /// </summary>
    public string[] HexCodes
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

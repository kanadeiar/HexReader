namespace HexReader.Infrastructure.ViewModels;

public class MainWindowViewModel : ViewModel
{
    #region Data

    private readonly IGetDataService _getDataService;

    #endregion

    #region Properties

    public ObservableCollection<BinaryRecord> BinaryRecords { get; } = new();

    private string _InputOffset = "0";
    /// <summary>
    /// Смещение, задаваемое пользователем
    /// </summary>
    public string InputOffset
    {
        get => _InputOffset;
        set => Set(ref _InputOffset, value);
    }

    private long _CountLines;
    /// <summary>
    /// Количество строк в файле
    /// </summary>
    public long CountLines
    {
        get => _CountLines;
        set => Set(ref _CountLines, value);
    }

    private long _FileSize;
    /// <summary>
    /// Длинна файла
    /// </summary>
    public long FileSize
    {
        get => _FileSize;
        set => Set(ref _FileSize, value);
    }

    private string _FileName;
    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName
    {
        get => _FileName;
        set => Set(ref _FileName, value);
    }

    private long _ScrollValue;
    /// <summary>
    /// Значение скроллирования файла
    /// </summary>
    public long ScrollValue
    {
        get => _ScrollValue;
        set
        {
            if (Set(ref _ScrollValue, value))
            {
                if (isRefreshes)
                {
                    return;
                }
                Refresh(Convert.ToInt32(ScrollValue));
            }
        }
    }

    private string _Title = "Приложение чтения файлов в бинарном виде";
    /// <summary> Заголовок </summary>
    public string Title
    {
        get => _Title;
        set => Set(ref _Title, value);
    }

    #endregion

    public MainWindowViewModel(IGetDataService getDataService)
    {
        _getDataService = getDataService;      
    }

    #region Commands

    private ICommand? _OpenFileCommand;
    /// <summary> 
    /// Открыть файл для чтения 
    /// </summary>
    public ICommand OpenFileCommand => _OpenFileCommand ??=
        new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommand);
    private bool CanOpenFileCommand(object p) => true;
    private void OnOpenFileCommandExecuted(object p)
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                var count = _getDataService.GetFileSize(openFileDialog.FileName);
                if (count > 0)
                {
                    FileSize = count;
                    CountLines = count / 16;
                    if (count % 16 > 0)
                        CountLines++;
                    FileName = openFileDialog.FileName;
                    InputOffset = "0";
                    Refresh(Convert.ToInt32(ScrollValue));
                }
                else
                {
                    MessageBox.Show("Пустой файл");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть файл или ошибка обработки данных, ошибка: {ex.Message}", "Ошибка отрытия файла");
                throw;
            }
        }
    }

    private ICommand? _GoToOffsetCommand;
    /// <summary> 
    /// Перейти по указанному смещению 
    /// </summary>
    public ICommand GoToOffsetCommand => _GoToOffsetCommand ??=
        new LambdaCommand(OnGoToOffsetCommandExecuted, CanGoToOffsetCommand);
    private bool CanGoToOffsetCommand(object p) => FileName != default && int.TryParse((string)p, out _);
    private void OnGoToOffsetCommandExecuted(object p)
    {
        var offset = int.Parse((string)p);
        if (offset < 0)
        {
            MessageBox.Show("Значение смещения нужно указывать положительным числом");
            return;
        }
        ScrollValue = offset;
    }

    private ICommand? _GoToStartCommand;
    /// <summary> 
    /// Перейти на начальную позицию 
    /// </summary>
    public ICommand GoToStartCommand => _GoToStartCommand ??=
        new LambdaCommand(OnGoToStartCommandExecuted, CanGoToStartCommand);
    private bool CanGoToStartCommand(object p) => FileName != default;
    private void OnGoToStartCommandExecuted(object p)
    {
        if (isRefreshes)
        {
            return;
        }
        try
        {
            Refresh(0);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка обновления данных в приложении {ex.Message}", "Ошибка обновления");
            throw;
        }
    }

    private ICommand? _CloseAppCommand;
    /// <summary> 
    /// Закрыть приложение 
    /// </summary>
    public ICommand CloseAppCommand => _CloseAppCommand ??=
        new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
    private bool CanCloseAppCommandExecute(object p) => true;
    private void OnCloseAppCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private ICommand _ShowAboutCommand;
    /// <summary> 
    /// Открыть приложение о программе 
    /// </summary>
    public ICommand ShowAboutCommand => _ShowAboutCommand ??=
        new LambdaCommand(OnShowAboutCommandExecuted, CanShowAboutCommandExecute);
    private bool CanShowAboutCommandExecute(object p) => true;
    private void OnShowAboutCommandExecuted(object p)
    {
        var form = new About();
        form.ShowDialog();
    }

    #endregion

    #region Support

    /// <summary>
    /// Происходит обновление данных
    /// </summary>
    private bool isRefreshes;

    /// <summary>
    /// Обновить данные в приложении
    /// </summary>
    /// <param name="offset"></param>
    private void Refresh(int offset)
    {
        isRefreshes = true;
        var data = _getDataService.GetLinesDataFromFile(FileName, offset);
        BinaryRecords.Clear();
        foreach (var item in data)
        {
            BinaryRecords.Add(item);
        }
        isRefreshes = false;
    }

    #endregion
}

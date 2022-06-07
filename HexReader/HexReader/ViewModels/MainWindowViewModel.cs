using HexReader.CoreDomain.Models;

namespace HexReader.Infrastructure.ViewModels;

public class MainWindowViewModel : ViewModel
{
    #region Data

    private readonly IGetDataService _getDataService;

    #endregion

    #region Properties

    public ObservableCollection<BinaryRecord> BinaryRecords { get; } = new();

    private string _Offset = "0";
    public string Offset
    {
        get => _Offset;
        set => Set(ref _Offset, value);
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

        Refresh(0);
    }

    #region Commands

    private ICommand? _GoToOffsetCommand;
    /// <summary> Перейти по указанному смещению </summary>
    public ICommand GoToOffsetCommand => _GoToOffsetCommand ??=
        new LambdaCommand(OnGoToOffsetCommandExecuted, CanGoToOffsetCommand);
    private bool CanGoToOffsetCommand(object p) => int.TryParse((string)p, out _);
    private void OnGoToOffsetCommandExecuted(object p)
    {
        var offset = int.Parse((string)p);
        if (offset < 0)
        {
            MessageBox.Show("Значение смещения нужно указывать положительным числом");
            return;
        }
        Refresh(offset);
    }

    private ICommand? _GoToStartCommand;
    /// <summary> Перейти на начальную позицию </summary>
    public ICommand GoToStartCommand => _GoToStartCommand ??=
        new LambdaCommand(OnGoToStartCommandExecuted, CanGoToStartCommand);
    private bool CanGoToStartCommand(object p) => true;
    private void OnGoToStartCommandExecuted(object p)
    {
        Refresh(0);
    }

    private ICommand? _CloseAppCommand;
    /// <summary> Закрыть приложение </summary>
    public ICommand CloseAppCommand => _CloseAppCommand ??=
        new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
    private bool CanCloseAppCommandExecute(object p) => true;
    private void OnCloseAppCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private ICommand _ShowAboutCommand;
    /// <summary> Открыть приложение о программе </summary>
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

    private void Refresh(int offset)
    {
        BinaryRecords.Clear();
        var data = _getDataService.GetLinesDataFromFile("test2.dat", offset);
        foreach (var item in data)
        {
            BinaryRecords.Add(item);
        }
    }

    #endregion
}

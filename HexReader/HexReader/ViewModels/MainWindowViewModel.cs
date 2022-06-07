using HexReader.CoreDomain.Models;

namespace HexReader.Infrastructure.ViewModels;

public class MainWindowViewModel : ViewModel
{
    #region Data

    private readonly IGetDataService _getDataService;

    #endregion

    #region Properties

    public ObservableCollection<BinaryRecord> BinaryRecords { get; } = new();

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

        Refresh();
    }

    #region Commands

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

    private void Refresh()
    {
        BinaryRecords.Clear();
        var data = _getDataService.GetLinesDataFromFile("test2.dat", 0);
        foreach (var item in data)
        {
            BinaryRecords.Add(item);
        }
    }

    #endregion
}

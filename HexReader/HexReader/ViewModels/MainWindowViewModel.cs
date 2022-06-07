namespace HexReader.Infrastructure.ViewModels;

public class MainWindowViewModel : ViewModel
{
    #region Data

    #endregion

    #region Properties

    private string _Title = "Приложение чтения файлов в бинарном виде";
    /// <summary> Заголовок </summary>
    public string Title
    {
        get => _Title;
        set => Set(ref _Title, value);
    }

    #endregion

    public MainWindowViewModel()
    {

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



    #endregion
}

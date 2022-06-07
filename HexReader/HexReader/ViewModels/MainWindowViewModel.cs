using HexReader.CoreDomain.Models;
using Microsoft.Win32;

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

    private long _CountLines;
    public long CountLines
    {
        get => _CountLines;
        set => Set(ref _CountLines, value);
    }

    private long _FileSize;
    public long FileSize
    {
        get => _FileSize;
        set => Set(ref _FileSize, value);
    }


    private string _FileName;
    public string FileName
    {
        get => _FileName;
        set => Set(ref _FileName, value);
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
        //try
        //{
        //    Refresh(0);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show($"Ошибка обновления данных: {ex.Message}", "Ошибка");
        //    throw;
        //}        
    }

    #region Commands

    private ICommand? _OpenFileCommand;
    /// <summary> Открыть файл для чтения </summary>
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
                var count = _getDataService.GetFileCountLines(openFileDialog.FileName);
                if (count > 0)
                {
                    FileSize = count;
                    CountLines = count / 16;
                    if (count % 16 > 0)
                        CountLines++;
                    FileName = openFileDialog.FileName;
                    Offset = "0";
                    Refresh(0);
                }
                else
                {
                    MessageBox.Show("Пустой файл");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть файл, ошибка: {ex.Message}", "Ошибка отрытия файла");
                throw;
            }
        }
    }

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
        try
        {
            Refresh(offset);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка обновления данных: {ex.Message}", "Ошибка");
            throw;
        }        
    }

    private ICommand? _GoToStartCommand;
    /// <summary> Перейти на начальную позицию </summary>
    public ICommand GoToStartCommand => _GoToStartCommand ??=
        new LambdaCommand(OnGoToStartCommandExecuted, CanGoToStartCommand);
    private bool CanGoToStartCommand(object p) => true;
    private void OnGoToStartCommandExecuted(object p)
    {
        try
        {
            Refresh(0);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка обновления данных: {ex.Message}", "Ошибка");
            throw;
        }
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
        var data = _getDataService.GetLinesDataFromFile(FileName, offset);
        foreach (var item in data)
        {
            BinaryRecords.Add(item);
        }
    }

    #endregion
}

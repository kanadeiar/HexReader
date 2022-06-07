namespace HexReader.ViewModels;

public class ViewModelLocator
{
    /// <summary>
    /// Вьюмодель главного окна
    /// </summary>
    public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
}

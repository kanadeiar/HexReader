using System.Text;

namespace HexReader;

public partial class App : Application
{
    private static IServiceProvider? __Services;
    private static IServiceCollection GetServices()
    {
        var services = new ServiceCollection();
        InitServices(services);
        return services;
    }

    private static void InitServices(IServiceCollection services)
    {
        services.AddScoped<MainWindowViewModel>();

        services.AddScoped<IFileReaderHelper, BinaryFileReaderHelper>();
        services.AddScoped<IGetDataService, GetBinaryDataService>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    /// <summary> 
    /// Сервисы 
    /// </summary>
    public static IServiceProvider Services => __Services ??= GetServices()
        .BuildServiceProvider();
}

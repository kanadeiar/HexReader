namespace HexReader.Windows;

public partial class About : Window
{
    public About()
    {
        InitializeComponent();
    }

    private void GitHubHyperLink_Click(object sender, RoutedEventArgs e)
    {
        var destinationurl = "https://github.com/kanadeiar/HexReader";
        var sInfo = new ProcessStartInfo(destinationurl)
        {
            UseShellExecute = true,
        };
        Process.Start(sInfo);
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

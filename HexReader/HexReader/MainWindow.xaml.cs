namespace HexReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainScollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            if (e.ScrollEventType == System.Windows.Controls.Primitives.ScrollEventType.SmallIncrement && MainScollBar.Value < MainScollBar.Maximum)
            {
                MainScollBar.Value++;
            }
            if (e.ScrollEventType == System.Windows.Controls.Primitives.ScrollEventType.SmallDecrement && MainScollBar.Value > MainScollBar.Minimum)
            {
                MainScollBar.Value--;
            }
            if (e.ScrollEventType == System.Windows.Controls.Primitives.ScrollEventType.LargeIncrement && MainScollBar.Value + 40 < MainScollBar.Maximum)
            {
                MainScollBar.Value += 40;
            }
            if (e.ScrollEventType == System.Windows.Controls.Primitives.ScrollEventType.LargeDecrement && MainScollBar.Value - 40 > MainScollBar.Minimum)
            {
                MainScollBar.Value -= 40;
            }
        }

        private void RecordsListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0 && MainScollBar.Value < MainScollBar.Maximum)
            {
                MainScollBar.Value++;
            }
            if (e.Delta > 0 && MainScollBar.Value > MainScollBar.Minimum)
            {
                MainScollBar.Value--;
            }
        }
    }
}

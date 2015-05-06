using System.Windows;
using WorkTimer.Properties;

namespace WorkTimer
{
    public partial class MainWindow
    {
        private readonly WorkTimerController _controller;

        public MainWindow()
        {
            InitializeComponent();

            _controller = new WorkTimerController(Settings.Default.MaxWorkDurationBeforeBreak);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _controller.WorkPeriodStarted();
        }
    }
}
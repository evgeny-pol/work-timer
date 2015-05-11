using Microsoft.Win32;
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

            SystemEvents.SessionSwitch += HandleSystemSessionSwitch;
        }

        private void OnLoaded(object sender, RoutedEventArgs eventArgs)
        {
            WorkStarted();
        }

        private void HandleSystemSessionSwitch(object sender, SessionSwitchEventArgs eventArgs)
        {
            /*
             * SessionSwitchReason:
             * ConsoleConnect       - session has been connected from the console.
             * ConsoleDisconnect    - session has been disconnected from the console.
             * RemoteConnect        - session has been connected from a remote connection.
             * RemoteDisconnect     - session has been disconnected from a remote connection.
             * SessionLock          - session has been locked.
             * SessionLogoff        - user has logged off from a session.
             * SessionLogon         - user has logged on to a session.
             * SessionRemoteControl - session has changed its status to or from remote controlled mode.
             * SessionUnlock        - session has been unlocked.
             */

            // todo: test in multi-user environment

            switch (eventArgs.Reason)
            {
                case SessionSwitchReason.SessionUnlock:
                    WorkStarted();
                    break;

                case SessionSwitchReason.SessionLock:
                    WorkSuspended();
                    break;
            }
        }

        private void WorkStarted()
        {
            _controller.WorkPeriodStarted();
        }

        private void WorkSuspended()
        {
            _controller.WorkPeriodEnded();
        }
    }
}
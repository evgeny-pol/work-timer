using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using WorkTimer.Properties;
using Resource = WorkTimer.Properties.Resources;

namespace WorkTimer
{
    public partial class MainWindow
    {
        private readonly WorkTimerController _controller;

        private NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            CreateNotifyIcon();

            _controller = new WorkTimerController(Settings.Default.MaxWorkDurationBeforeBreak);

            SystemEvents.SessionSwitch += HandleSystemSessionSwitch;
        }

        private void OnLoaded(object sender, RoutedEventArgs eventArgs)
        {
            WorkStarted();
        }

        private void HandleSystemSessionSwitch(object sender, SessionSwitchEventArgs eventArgs)
        {
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

        private void CreateNotifyIcon()
        {
            if (_notifyIcon != null)
                return;

            _notifyIcon = new NotifyIcon
            {
                Text = Resource.TrayIconText,
                Icon = Resource.TrayIcon,
                Visible = true
            };
        }
    }
}
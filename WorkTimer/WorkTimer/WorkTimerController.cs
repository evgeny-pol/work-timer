using System;
using System.Threading;

namespace WorkTimer
{
    public sealed class WorkTimerController
    {
        private const double TimerPeriodMinutes = 1;

        public event Action<WorkTimerController, BreakRequiredEventArgs> BreakRequired;

        private readonly TimeSpan _maxWorkPeriodDuration;
        private readonly Timer _timer;

        /// <summary>
        /// Value that indicates when current work period started
        /// NULL value means that there is no work currently underway
        /// </summary>
        private DateTime? _currentWorkPeriodStart;

        public WorkTimerController(TimeSpan workDurationBeforeBreak)
        {
            _maxWorkPeriodDuration = workDurationBeforeBreak;

            _timer = new Timer(TimerCallback,
                               null,
                               TimeSpan.FromMilliseconds(0),
                               TimeSpan.FromMinutes(TimerPeriodMinutes));
        }

        public void WorkPeriodStarted()
        {
            _currentWorkPeriodStart = DateTime.Now;
        }

        public void WorkPeriodEnded()
        {
            _currentWorkPeriodStart = null;
        }

        private void TimerCallback(object state)
        {
            if (_currentWorkPeriodStart == null)
            {
                return;
            }

            var now = DateTime.Now;
            var currentWorkPeriodDuration = now - _currentWorkPeriodStart;

            if (currentWorkPeriodDuration > _maxWorkPeriodDuration)
            {
                var eventArgs = new BreakRequiredEventArgs(currentWorkPeriodDuration.Value);
                BreakRequired.Call(this, eventArgs);
            }
        }
    }
}
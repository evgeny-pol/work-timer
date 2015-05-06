using System;

namespace WorkTimer
{
    /// <summary>
    /// Class is NOT thread safe
    /// </summary>
    public class WorkTimerController
    {
        private readonly TimeSpan _maxWorkPeriodDuration;

        /// <summary>
        /// Value that indicates when current work period started
        /// NULL value means that there is no work currently underway
        /// </summary>
        private DateTime? _currentWorkPeriodStart;

        public WorkTimerController(TimeSpan workDurationBeforeBreak)
        {
            _maxWorkPeriodDuration = workDurationBeforeBreak;
        }

        public void WorkPeriodStarted()
        {
            _currentWorkPeriodStart = DateTime.Now;
        }

        public void WorkPeriodEnded()
        {
            _currentWorkPeriodStart = null;
        }
    }
}
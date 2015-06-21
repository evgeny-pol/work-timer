using System;

namespace WorkTimer
{
    public sealed class BreakRequiredEventArgs
    {
        public TimeSpan CurrentWorkPeriodDuration { get; private set; }

        public BreakRequiredEventArgs(TimeSpan currentWorkPeriodDuration)
        {
            CurrentWorkPeriodDuration = currentWorkPeriodDuration;
        }
    }
}
using System;
using System.Threading;

namespace WorkTimer
{
    public static class EventHelper
    {
        public static void Call<T1, T2>(this Action<T1, T2> @event, T1 arg1, T2 arg2)
        {
            var eventValue = Interlocked.CompareExchange(ref @event, null, null);

            if (eventValue != null)
            {
                eventValue(arg1, arg2);
            }
        }
    }
}
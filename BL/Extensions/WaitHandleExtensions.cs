using System;
using System.Threading;

namespace MyStore.Server
{
    internal static class WaitHandleExtensions
    {
        public static void SafeSet(this EventWaitHandle eventWaitHandle) 
        {
            if (!eventWaitHandle.SafeWaitHandle.IsClosed)
                eventWaitHandle.Set();
        } 
    }
}

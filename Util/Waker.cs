using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace sleepwake.Util
{
    public class Waker
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern SafeWaitHandle CreateWaitableTimer(IntPtr lpTimerAttributes, bool bManualReset, string lpTimerName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWaitableTimer(SafeWaitHandle hTimer, [In] ref long pDueTime, int lPeriod, IntPtr pfnCompletionRoutine, IntPtr pArgToCompletionRoutine, bool fResume);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CancelWaitableTimer(SafeWaitHandle hTimer);

        public static Task<SafeWaitHandle> SetWakeAt(DateTime dt)
        {
            return Task.Run(() =>
            {
                // read the manual for SetWaitableTimer to understand how this number is interpreted.
                // long interval = dt.ToFileTimeUtc();
                var waketime = dt.ToFileTime();

                using (var handle = CreateWaitableTimer(IntPtr.Zero, true, "sleepwakeWakeUpTimer"))
                {
                    if (SetWaitableTimer(handle, ref waketime, 0, IntPtr.Zero, IntPtr.Zero, true))
                    {
                        using (EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset))
                        {
                            wh.SafeWaitHandle = handle;
                            wh.WaitOne();
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    return handle;
                }
            });
        }

        public static bool CancelWake(SafeWaitHandle hTimer)
        {
            return CancelWaitableTimer(hTimer);
        }
    }
}

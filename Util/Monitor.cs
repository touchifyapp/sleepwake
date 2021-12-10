using System;
using System.Runtime.InteropServices;

namespace sleepwake.Util
{
    public class Monitor
    {
        #region SetThreadExecutionState P/Invoke (New Systems)

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ThreadExecutionState SetThreadExecutionState(ThreadExecutionState esFlags);

        [FlagsAttribute]
        public enum ThreadExecutionState : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        #endregion

        #region SendMessage P/Invoke (Old Systems)

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private static int MONITOR_ON = -1;
        private static int MONITOR_OFF = 2;
        private static int MONITOR_STANBY = 1;

        private static IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        private static UInt32 WM_SYSCOMMAND = 0x0112;
        private static IntPtr SC_MONITORPOWER = new IntPtr(0xF170);

        #endregion

        /** Works on Windows XP+ Systems */
        public static void Resume()
        {
            SetThreadExecutionState(ThreadExecutionState.ES_DISPLAY_REQUIRED);
        }

        /** /!\ Old Systems */
        public static void On()
        {
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, (IntPtr)MONITOR_ON);
        }

        /** /!\ Old Systems */
        public static void Off()
        {
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, (IntPtr)MONITOR_OFF);
        }

        /** /!\ Old Systems */
        public static void Stanby()
        {
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, (IntPtr)MONITOR_STANBY);
        }
    }
}

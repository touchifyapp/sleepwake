using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace sleepwake.Util
{
    public class Sleeper
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        public static bool Hibernate()
        {
            return SetSuspendState(true, false, false);
        }

        public static bool Sleep()
        {
            return SetSuspendState(false, false, false);
        }

        public static void SetSleepAt(DateTime dt)
        {
            Task.Delay(dt - DateTime.Now).ContinueWith(_ => Sleep());
        }

        public static void SetHibernateAt(DateTime dt)
        {
            Task.Delay(dt - DateTime.Now).ContinueWith(_ => Hibernate());
        }
    }
}

using System;
using CommandLine;

using sleepwake.Util;

namespace sleepwake
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                var options = OptionsParser.ParseOptions(o);

                if (options.Verbose)
                    Console.WriteLine($"Time: {options.Wake}, Sleep: {options.Sleep}, Hibernate: {options.Hibernate}");

                var task = Waker.SetWakeAt(DateTime.Now.Add(options.Wake));

                if (options.Sleep)
                    Sleeper.Sleep();

                if (options.Hibernate)
                    Sleeper.Hibernate();

                task.Wait();

                Monitor.Resume();
            });
        }
    }
}

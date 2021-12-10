using System;
using CommandLine;

namespace sleepwake.Util
{
    public class Options
    {
        [Option('w', "wake", Required = true, HelpText = "Set wake duration. Format (00:00).")]
        public string Wake { get; set; }

        [Option('s', "sleep", Required = false, HelpText = "Enable sleep.")]
        public bool Sleep { get; set; }

        [Option('h', "hibernate", Required = false, HelpText = "Enable hibernate.")]
        public bool Hibernate { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Verbose logging.")]
        public bool Verbose { get; set; }
    }

    public class ParsedOptions
    {
        public TimeSpan Wake { get; set; }

        public bool Sleep { get; set; }

        public bool Hibernate { get; set; }

        public bool Verbose { get; set; }
    }
    public class OptionsParser
    {
        public static ParsedOptions ParseOptions(Options options)
        {
            if (string.IsNullOrEmpty(options.Wake))
            {
                Console.Error.WriteLine("Missing required argument: --wake");
                Environment.Exit(1);
            }

            if (options.Sleep && options.Hibernate)
            {
                Console.Error.WriteLine("You can't provide both --sleep and --hibernate arguments!");
                Environment.Exit(1);
            }

            return new ParsedOptions
            {
                Wake = ParseTimeOption("wake", options.Wake),
                Sleep = options.Sleep,
                Hibernate = options.Hibernate,
                Verbose = options.Verbose
            };
        }

        private static TimeSpan ParseTimeOption(string name, string option)
        {
            try
            {
                return TimeSpan.Parse(option);
            }
            catch
            {
                Console.Error.WriteLine($"Misformatted argument: --{name} ! Expected format: 00:00(:00).");

                Environment.Exit(1);
                throw new Exception("Bad format");
            }
        }
    }
}
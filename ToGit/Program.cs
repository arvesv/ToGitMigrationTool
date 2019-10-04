using CommandLine;

namespace ToGit
{
    class Program
    {
        public class Options
        {

            [Option('s', "setupfile", Required = true, HelpText = "Path to setup file")]
            public string SetupFile { get; set; }

            public Options()
            {
                SetupFile = "";   // Just to please the C# 8 nullable checks
            }
        }


        static void Main(string[] args)
        {
            Config? cfg = null;

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                    cfg = Config.ReadFile(o.SetupFile)
                );
        }
    }
}

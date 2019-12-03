using CommandLine;
using System.Collections.Generic;
using System.IO;

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
                {
                    cfg = Config.ReadFile(o.SetupFile);
                    var tfs = new Tfs(cfg.TfsUrl, cfg.PersonalAccessToken, cfg.Map, cfg.WorkingFolder);

                    int fromChangeSet = int.Parse(File.ReadAllText(cfg.StateFile))+1;


                    List<int> changsets = new List<int>();

                    foreach(var map in cfg.Map)
                    {
                        var q = tfs.GetChangesetsForPath(map.TfsPath, fromChangeSet);
                        changsets.AddRange(q);
                    }

                    changsets.Sort();

                    foreach(int changesetid in changsets)
                    {
                        var changeset = tfs.GetChangeset(changesetid);


                    
                    
                    }








                }
                );
        }
    }
}

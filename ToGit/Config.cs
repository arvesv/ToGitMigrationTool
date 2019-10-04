using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace ToGit
{
    public class TfsMapping
    {
        public TfsMapping(string tfsPath, string relLocalPath)
        {
            TfsPath = tfsPath;
            RealiveLocalPath = relLocalPath;
        }

        string TfsPath;
        string RealiveLocalPath;
    }


    public class Config
    {
        public string WorkingFolder;
        public string TfsUrl;
        IEnumerable<TfsMapping> Map;


        public static Config ReadFile(string filename)
        {
            using var reader =  new StreamReader(filename);

            var deserializer = new DeserializerBuilder().Build();
            var res = deserializer.Deserialize<dynamic>(reader);

            return new Config(res["workdirectory"])
            {
                TfsUrl = res["tfs"]["url"]
            };
        }

        private Config(string workingFoder)
        {
            WorkingFolder = workingFoder;
            TfsUrl = "";
            Map = new List<TfsMapping>();
        }
    }
}

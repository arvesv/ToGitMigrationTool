using System.Collections.Generic;
using System.IO;
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

        public string TfsPath;
        public string RealiveLocalPath;
    }


    public class Config
    {
        public string WorkingFolder;
        public string TfsUrl;
        public string PersonalAccessToken;
        public string StateFile;
        public IList<TfsMapping> Map;

        public static Config ReadFile(string filename)
        {
            using var reader = new StreamReader(filename);
            var deserializer = new DeserializerBuilder().Build();
            var res = deserializer.Deserialize<dynamic>(reader);

            var mapping = res["tfs"]["mapping"];
            var newMap = new List<TfsMapping>(mapping.Count);

            foreach (var map in mapping)
            {
                newMap.Add(new TfsMapping(map.Value, map.Key));
            }

            return new Config(res["workdirectory"], res["tfs"]["url"], newMap, res["tfs"]["pac"], res["statefile"]);
        }

        private Config(string workingFoder, string tfsUrl, IList<TfsMapping> map, string accesstoken, string statefile)
        {
            WorkingFolder = workingFoder;
            TfsUrl = tfsUrl;
            Map = map;
            PersonalAccessToken = accesstoken;
            StateFile = statefile;
        }
    }
}

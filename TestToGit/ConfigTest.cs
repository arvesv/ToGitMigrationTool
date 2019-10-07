using ToGit;
using Xunit;

namespace TestToGit
{
    public class ConfigTest
    {
        readonly string testYmlFile = "testconfig.yml";

        [Fact]
        public void TestReadConfigYaml()
        {
            // Testing that we can read the file above
            var cfg = Config.ReadFile(testYmlFile);

            Assert.Equal(@"b:\my_dir\hei", cfg.WorkingFolder);
            Assert.Equal(@"https://my_serv/tfs", cfg.TfsUrl);

            Assert.Equal(1, cfg.Map.Count);
            Assert.Equal("dir1", cfg.Map[0].RealiveLocalPath);
            Assert.Equal("$/Project1/Main", cfg.Map[0].TfsPath);
        }
    }
}

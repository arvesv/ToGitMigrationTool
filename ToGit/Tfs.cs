using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToGit
{
    public class Tfs
    {
        string Url;
        string _personalAccessToken;
        IList<TfsMapping> Map;
        string localbasepath;

        Lazy<TfvcHttpClient> tfvc = new Lazy<TfvcHttpClient>();
          


        public Tfs(string url, string pac, IList<TfsMapping> map, string basepath)
        {
            Url = url;
            _personalAccessToken = pac;
            Map = map;
            localbasepath = basepath;

            tfvc = new Lazy<TfvcHttpClient>(() => {
                VssBasicCredential credentials = new VssBasicCredential("", _personalAccessToken);
                var conn = new VssConnection(new Uri("https://team47system1.corp.u4agr.com/tfs/DefaultCollection/"), credentials);
                return conn.GetClient<TfvcHttpClient>();
            });
        }


        public IEnumerable<int> GetChangesets(int startChanegsetId)
        {
           
            TfvcChangesetSearchCriteria crit = new TfvcChangesetSearchCriteria
            {
                ItemPath = "$/ATF/Main/ACT",
            };

            var z = tfvc.Value.GetChangesetsAsync(searchCriteria: crit).Result;

            return new List<int>();
        }
    }
}

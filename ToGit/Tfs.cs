using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToGit
{
    public class ChangeSet
    {
        public string Comment;
        public string Email;
        public string Name;
        public int? WorkItem;
        public int Id;
    }


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

            tfvc = new Lazy<TfvcHttpClient>(() =>
            {
                VssBasicCredential credentials = new VssBasicCredential("", _personalAccessToken);
                var conn = new VssConnection(new Uri(Url), credentials);
                return conn.GetClient<TfvcHttpClient>();
            });
        }

        public IEnumerable<int> GetChangesetsForPath(string tfspath, int startChangesetId)
        {
            List<int> ret = new List<int>();
            int skip = 0;
            const int maxresult = 100;
            int noFetched = 0;

            TfvcChangesetSearchCriteria crit = new TfvcChangesetSearchCriteria
            {
                ItemPath = tfspath,
                FromId = startChangesetId
            };

            do
            {
                var wsResult = tfvc.Value.GetChangesetsAsync(searchCriteria: crit, skip: skip).Result;
                noFetched = wsResult.Count;
                skip += noFetched;
                ret.AddRange(wsResult.Select(c => c.ChangesetId));
            }
            while (noFetched == maxresult);
            return ret;
        }

        internal ChangeSet GetChangeset(int changesetid)
        {
            var changeset = tfvc.Value.GetChangesetAsync(changesetid, includeWorkItems: true, includeDetails: true).Result;

            int? wi = null;

            if(changeset.WorkItems.Count() > 0)
            {
                wi = changeset.WorkItems.First().Id;
            }
            return new ChangeSet
            {
                Comment = changeset.Comment,
                Name = changeset.Author.DisplayName,
                WorkItem = wi,
                Email = "<>",
                Id = changesetid
            };
        }

        public IEnumerable<int> GetChangesets(int startChanegsetId)
        {
            TfvcChangesetSearchCriteria crit = new TfvcChangesetSearchCriteria
            {
                ItemPath = "$/Platform/Main"
            };

            var z = tfvc.Value.GetChangesetsAsync(searchCriteria: crit).Result;

            return new List<int>();
        }
    }
}

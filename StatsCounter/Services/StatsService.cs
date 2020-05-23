using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public interface IStatsService
    {
        Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
    }
    
    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;

        public StatsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            var repos = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);

            return ProcessGitHubData(repos, owner);
        }

        private RepositoryStats ProcessGitHubData(IEnumerable<RepositoryInfo> repos, string owner)
        {
            var result = new RepositoryStats();
            result.Owner = owner;

            result.AvgSize = repos.Average(x => x.Size);
            result.AvgForks = repos.Average(x => x.ForksCount);
            result.AvgStargazers = repos.Average(x => x.StargazersCount);
            result.AvgWatchers = repos.Average(x => x.WatchersCount);

            result.Letters = new Dictionary<char, int>();

            foreach(var repo in repos)
            {
                foreach (var c in repo.Name.ToLower())
                {
                    if (!result.Letters.ContainsKey(c))
                    {
                        result.Letters.Add(c, 0);
                    }

                    result.Letters[c]++;
                }
            }

            return result;
        }
    }
}
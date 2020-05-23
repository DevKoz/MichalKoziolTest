using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner);
    }
    
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
            var result = await _httpClient.GetAsync("/users/" + owner + "/repos");

            var content = result.Content.ReadAsStringAsync();
            var repos = JsonConvert.DeserializeObject<IEnumerable<RepositoryInfo>>(content.Result);

            return repos;
        }
    }
}

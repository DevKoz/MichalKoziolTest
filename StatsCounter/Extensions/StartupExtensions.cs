using System;
using Microsoft.Extensions.DependencyInjection;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services,
            Uri baseApiUrl)
        {
            throw new NotImplementedException(); // TODO: add your code here
        }
    }
}
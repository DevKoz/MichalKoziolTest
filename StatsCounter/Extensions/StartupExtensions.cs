using System;
using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Services;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services,
            Uri baseApiUrl)
        {
            services.AddHttpClient<IGitHubService, GitHubService>("GitHubClient",
                client =>
                {
                    client.BaseAddress = baseApiUrl;
                });

            return services;
        }
    }
}
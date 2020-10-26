using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IdentityProvider.Application.Data
{
    public static class ConfigurationDataSeed
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var config = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                Seed(configContext);

                return app;
            }
        }

        private static void Seed(ConfigurationDbContext dbContext)
        {
            AddIdentityResources(dbContext);
            AddApiScopes(dbContext);
            dbContext.SaveChanges();
        }

        private static ICollection<string> GetScopes()
        {
            ICollection<string> scopes = new List<string>();
            scopes.Add(IdentityServerConstants.StandardScopes.OpenId);
            scopes.Add(IdentityServerConstants.StandardScopes.Profile);
            scopes.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
            scopes.Add(IdentityServerConstants.StandardScopes.Email);
            return scopes;
        }

        private static void AddIdentityResources(ConfigurationDbContext context)
        {
            foreach (var resource in GetResources())
            {
                var identityResource = context.IdentityResources.SingleOrDefault(x => x.Name == resource.Name);
                if (identityResource == null)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
            }
        }

        private static void AddApiScopes(ConfigurationDbContext context)
        {
            foreach (var api in GetApis())
            {
                var apiScope = context.ApiScopes.SingleOrDefault(x => x.Name == api.Name);
                if (apiScope == null)
                {
                    context.ApiScopes.Add(api.ToEntity());
                }
            }
        }

        private static IEnumerable<ApiScope> GetApis()
        {
            return new List<ApiScope>
            {
            };
        }

        private static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }
    }
}

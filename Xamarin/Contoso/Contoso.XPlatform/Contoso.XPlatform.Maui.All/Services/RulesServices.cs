using Contoso.Domain.Entities;
using Contoso.XPlatform.Flow.Rules;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class RulesServices
    {
        internal static IServiceCollection AddRulesCache(this IServiceCollection services)
        {
            IRulesCache cache = Task.Run(() => LoadRules(new RulesLoader())).GetAwaiter().GetResult();
            services.AddSingleton(sp => cache);
            return services;
        }

        static async Task<RulesCache> LoadRules(IRulesLoader rulesLoader)
        {
            RulesCache cache = new(new ConcurrentDictionary<string, RuleEngine>(), new ConcurrentDictionary<string, string>());

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPageViewModel)).Assembly;
            string[] uwpResources = GetResourceStrings(assembly);

            Dictionary<string, string> rules = uwpResources
                                                .Where(f => f.EndsWith(".module"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            Dictionary<string, string> resources = uwpResources
                                                .Where(f => f.EndsWith(".resources"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());


            DateTime dt = DateTime.Now;

            await Task.WhenAll
            (
                rules.Keys.Select
                (
                    key => LoadRules
                    (
                        new RulesModuleModel
                        {
                            Name = key,
                            ResourceSetFile = GetBytes(resources[key], assembly),
                            RuleSetFile = GetBytes(rules[key], assembly)
                        }
                    )
                )
            );

            return cache;

            Task LoadRules(RulesModuleModel module)
            {
                return Task.Run(() => rulesLoader.LoadRules(module, cache));
            }

            string GetKey(string fullResourceName)
                => Path.GetExtension(Path.GetFileNameWithoutExtension(fullResourceName)).Substring(1);
        }

        private static string[] GetResourceStrings(Assembly assembly)
         => assembly.GetManifestResourceNames()
                    .Where
                    (
                        res => res.StartsWith
                        (
                            $"Contoso.XPlatform.Rulesets.",
                            StringComparison.InvariantCultureIgnoreCase
                        )
                    ).ToArray();

        private static byte[] GetBytes(string file, Assembly assembly)
        {
            using (Stream platformStream = assembly.GetManifestResourceStream(file)!)
            {
                byte[] byteArray = new byte[platformStream.Length];
                platformStream.Read(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }
    }
}

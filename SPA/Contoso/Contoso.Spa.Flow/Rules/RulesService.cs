using Contoso.Domain.Entities;
using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Contoso.Spa.Flow.Rules
{
    public static class RulesService
    {
        public static async Task<RulesCache> LoadRules()
        {
            return await LoadRules(new RulesLoader());
        }

        static async Task<RulesCache> LoadRules(IRulesLoader rulesLoader)
        {
            RulesCache cache = new(new ConcurrentDictionary<string, RuleEngine>(), new ConcurrentDictionary<string, string>());

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FlowActivity)).Assembly;
            string[] embeddedResources = GetResourceNames(assembly);

            Dictionary<string, string> rules = embeddedResources
                                                .Where(f => f.EndsWith(".module"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            Dictionary<string, string> resources = embeddedResources
                                                .Where(f => f.EndsWith(".resources"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            await Task.WhenAll
            (
                rules.Keys.Select
                (
                    key => rulesLoader.LoadRulesOnStartUp
                    (
                        new RulesModuleModel
                        {
                            Name = key,
                            ResourceSetFile = GetBytes(resources[key], assembly),
                            RuleSetFile = GetBytes(rules[key], assembly)
                        },
                        cache
                    )
                )
            );

            return cache;

            static string GetKey(string fullResourceName)
                => Path.GetExtension(Path.GetFileNameWithoutExtension(fullResourceName))[1..];
        }

        private static string[] GetResourceNames(Assembly assembly)
         => assembly.GetManifestResourceNames()
                    .Where
                    (
                        res => res.StartsWith
                        (
                            "Contoso.Spa.Flow.Rulesets.",
                            System.StringComparison.InvariantCultureIgnoreCase
                        )
                    ).ToArray();

        private static byte[] GetBytes(string file, Assembly assembly)
        {
            using Stream platformStream = assembly.GetManifestResourceStream(file) ?? throw new ArgumentException($"{nameof(file)}: {{8C3D4D61-C98D-4A4A-BCF8-9AF7FD1D2547}}");
            byte[] byteArray = new byte[platformStream.Length];
            platformStream.Read(byteArray, 0, byteArray.Length);
            return byteArray;
        }
    }
}

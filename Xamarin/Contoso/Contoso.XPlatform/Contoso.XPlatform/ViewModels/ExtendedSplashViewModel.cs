using Contoso.Domain.Entities;
using Contoso.XPlatform.Flow.Rules;
using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels
{
    public class ExtendedSplashViewModel : ViewModelBase
    {
        private readonly IRulesLoader rulesLoader;
        private double _progress;

        public ExtendedSplashViewModel(IRulesLoader rulesLoader)
        {
            this.rulesLoader = rulesLoader;
        }

        public double Progress
        {
            get { return _progress; }
            set
            {
                if (_progress == value)
                    return;

                _progress = value;
                this.OnPropertyChanged();
            }
        }

        public async Task AddRulesCacheService()
        {
            IRulesCache cache = await LoadRules(rulesLoader);
            App.ServiceCollection.AddSingleton<IRulesCache>(sp =>
            {
                return cache;
            });

        }

        async Task<RulesCache> LoadRules(IRulesLoader rulesLoader)
        {
            RulesCache cache = new RulesCache(new ConcurrentDictionary<string, RuleEngine>(), new ConcurrentDictionary<string, string>());

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ExtendedSplashViewModel)).Assembly;
            string[] uwpResources = GetResourceStrings(assembly);

            Dictionary<string, string> rules = uwpResources
                                                .Where(f => f.EndsWith(".module"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            Dictionary<string, string> resources = uwpResources
                                                .Where(f => f.EndsWith(".resources"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());


            int count = 0;
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
                return Task.Run(() =>
                {
                    rulesLoader.LoadRules(module, cache);
                    count++;
                    Progress = (double)count / rules.Count;
                });
            }

            string GetKey(string fullResourceName)
                => Path.GetExtension(Path.GetFileNameWithoutExtension(fullResourceName)).Substring(1);
        }

        private string[] GetResourceStrings(Assembly assembly)
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
            using (Stream platformStream = assembly.GetManifestResourceStream(file))
            {
                byte[] byteArray = new byte[platformStream.Length];
                platformStream.Read(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }
    }
}

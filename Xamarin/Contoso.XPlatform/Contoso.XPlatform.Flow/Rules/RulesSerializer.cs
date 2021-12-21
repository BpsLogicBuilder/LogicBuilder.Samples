using Contoso.Domain.Entities;
using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Contoso.XPlatform.Flow.Rules
{
    internal static class RulesSerializer
    {
        private static readonly List<Assembly> assemblies = new List<Assembly>
        {
            typeof(Forms.Parameters.CommandButtonParameters).Assembly,
            typeof(Forms.Configuration.CommandButtonDescriptor).Assembly,
            typeof(Parameters.Expansions.SortCollectionParameters).Assembly,
            typeof(Common.Configuration.ExpansionDescriptors.SortCollectionDescriptor).Assembly,
            typeof(Common.Utils.MappingOperations).Assembly,
            typeof(LogicBuilder.Forms.Parameters.ConnectorParameters).Assembly,
            typeof(Data.BaseDataClass).Assembly,
            typeof(Domain.BaseModelClass).Assembly,
            typeof(LogicBuilder.RulesDirector.DirectorBase).Assembly,
            typeof(string).Assembly
        };

        /// <summary>
        /// Get Validation
        /// </summary>
        /// <param name="ruleSet"></param>
        /// <returns></returns>
        public static RuleValidation GetValidation(RuleSet ruleSet)
        {
            if (ruleSet == null)
                throw new InvalidOperationException(Properties.Resources.ruleSetCannotBeNull);

            RuleValidation ruleValidation = new RuleValidation(typeof(FlowActivity), assemblies);
            if (!ruleSet.Validate(ruleValidation))
            {
                throw new ArgumentException
                (
                    string.Join
                    (
                        Environment.NewLine,
                        ruleValidation.Errors.Aggregate
                        (
                            new List<string>
                            {
                                string.Format
                                (
                                    CultureInfo.CurrentCulture,
                                    Properties.Resources.invalidRulesetFormat,
                                    ruleSet.Name
                                )
                            },
                            (list, next) =>
                            {
                                list.Add(next.ErrorText);
                                return list;
                            }
                        )
                    )
                );
            }

            return ruleValidation;
        }

        /// <summary>
        /// Returns a rule set given a RulesModuleModel record
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static RuleSet DeserializeRuleSetFile(this RulesModuleModel module)
        {
            using (StreamReader inStream = new StreamReader(new MemoryStream(module.RuleSetFile)))
                return DeserializeRuleSet(inStream.ReadToEnd());
        }

        private static RuleSet DeserializeRuleSet(string ruleSetXmlDefinition)
        {

            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            if (!string.IsNullOrEmpty(ruleSetXmlDefinition))
            {
                using (StringReader stringReader = new StringReader(ruleSetXmlDefinition))
                {
                    using (XmlTextReader reader = new XmlTextReader(stringReader))
                    {
                        return serializer.Deserialize(reader) as RuleSet;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}

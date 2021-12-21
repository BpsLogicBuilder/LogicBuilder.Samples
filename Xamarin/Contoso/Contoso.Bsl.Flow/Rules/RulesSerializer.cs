using Contoso.Domain.Entities;
using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace Contoso.Bsl.Flow.Rules
{
    public static class RulesSerializer
    {
        /// <summary>
        /// Deserialize rule set
        /// </summary>
        /// <param name="ruleSetXmlDefinition"></param>
        /// <returns></returns>
        public static RuleSet DeserializeRuleSet(string ruleSetXmlDefinition)
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

        /// <summary>
        /// Get Validation
        /// </summary>
        /// <param name="ruleSet"></param>
        /// <returns></returns>
        public static RuleValidation GetValidation(RuleSet ruleSet)
        {
            if (ruleSet == null)
                throw new InvalidOperationException(Properties.Resources.ruleSetCannotBeNull);

            List<System.Reflection.Assembly> assemblies = new List<System.Reflection.Assembly>
            {
                typeof(Contoso.Parameters.Expansions.SelectExpandDefinitionParameters).Assembly,
                //typeof(Forms.View.ViewBase).Assembly,
                //typeof(LogicBuilder.Forms.Parameters.InputFormParameters).Assembly,
                typeof(Domain.BaseModelClass).Assembly,
                typeof(LogicBuilder.RulesDirector.DirectorBase).Assembly,
                typeof(string).Assembly
            };

            RuleValidation ruleValidation = new RuleValidation(typeof(FlowActivity), assemblies);
            if (!ruleSet.Validate(ruleValidation))
            {
                List<string> errors = ruleValidation.Errors.Aggregate
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
                );

                throw new ArgumentException(string.Join(Environment.NewLine, errors));
            }

            return ruleValidation;
        }

        /// <summary>
        /// Returns a rule set given a RulesModuleModel record
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        internal static RuleSet DeserializeRuleSetFile(this RulesModuleModel module)
        {
            using (StreamReader inStream = new StreamReader(new MemoryStream(module.RuleSetFile)))
                return DeserializeRuleSet(inStream.ReadToEnd());
        }
    }
}

using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow.Cache
{
    public class FlowDataCache
    {
        #region Properties
        /// <summary>
        /// List of Symptoms
        /// </summary>
        public List<string> Symptoms { get; set; } = new List<string>();

        /// <summary>
        /// List of Diagnoses
        /// </summary>
        public List<string> Diagnoses { get; set; } = new List<string>();

        /// <summary>
        /// List of Treatments
        /// </summary>
        public List<string> Treatments { get; set; } = new List<string>();

        /// <summary>
        /// List of past screens/questions
        /// </summary>
        public List<MenuItem> DialogList { get; set; } = new List<MenuItem>();

        /// <summary>
        /// Automatic Variables
        /// </summary>
        public Variables Variables { get; set; } = new Variables();
        #endregion Properties

        public FlowDataCache Clone() 
            => new FlowDataCache
            {
                Symptoms = new List<string>(this.Symptoms),
                Diagnoses = new List<string>(this.Diagnoses),
                Treatments = new List<string>(this.Treatments),
                DialogList = new List<MenuItem>(this.DialogList),
                Variables = this.Variables.Clone()
            };

        public void Reset()
        {
            Symptoms.Clear();
            Diagnoses.Clear();
            Treatments.Clear();
            DialogList.Clear();
            Variables.ValuesDictionary.Clear();
        }


        public void Copy(FlowDataCache flowDataCache)
        {
            Symptoms.Clear();
            Symptoms.AddRange(flowDataCache.Symptoms);

            Diagnoses.Clear();
            Diagnoses.AddRange(flowDataCache.Diagnoses);

            Treatments.Clear();
            Treatments.AddRange(flowDataCache.Treatments);

            DialogList.Clear();
            DialogList.AddRange(flowDataCache.DialogList);

            Variables.ValuesDictionary.Clear();
            Variables.SetValues(flowDataCache.Variables.ValuesDictionary);
        }
    }
}

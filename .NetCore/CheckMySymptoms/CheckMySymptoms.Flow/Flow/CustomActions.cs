using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Forms.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.Flow
{
    public class CustomActions : ICustomActions
    {
        public CustomActions(FlowDataCache flowDataCache, IPatientDataRepository patientDataRepository)
        {
            this.flowDataCache = flowDataCache;
            this.patientDataRepository = patientDataRepository;
        }

        #region Fields
        private readonly FlowDataCache flowDataCache;
        private readonly IPatientDataRepository patientDataRepository;
        private const string PATIENTTABLE = "Patient";
        #endregion Fields

        public void AddDiagnosis(string diagnosis)
        {
            flowDataCache.Diagnoses.Add(diagnosis);
        }

        public void AddSymptom(string symptom)
        {
            if (!new HashSet<string>(flowDataCache.Symptoms).Contains(symptom))
                flowDataCache.Symptoms.Add(symptom);
        }

        public void AddTreatment(string treatment)
        {
            flowDataCache.Treatments.Add(treatment);
        }

        public void SavePatientData()
        {
            Task.Run
            (
                async () => await patientDataRepository.SaveData(flowDataCache.Variables.ValuesDictionary)
            ).Wait();
        }

        public void GetPatientData()
        {
            flowDataCache.Variables.SetValues
            (
                Task.Run
                (
                    async () => await patientDataRepository.GetData(flowDataCache.Variables.ValuesDictionary)
                ).Result
            );
        }

        public string ConvertToString(object obj) => obj == null ? string.Empty : obj.ToString();
    }
}

using LogicBuilder.Attributes;

namespace CheckMySymptoms.Flow
{
    public interface ICustomActions
    {
        [AlsoKnownAs("AddSymptom")]
        void AddSymptom([ParameterEditorControl(ParameterControlType.MultipleLineTextBox)]string symptom);

        [AlsoKnownAs("AddDiagnosis")]
        void AddDiagnosis([ParameterEditorControl(ParameterControlType.MultipleLineTextBox)]string diagnosis);

        [AlsoKnownAs("AddTreatment")]
        void AddTreatment([ParameterEditorControl(ParameterControlType.MultipleLineTextBox)]string treatment);

        [AlsoKnownAs("SavePatientData")]
        void SavePatientData();

        [AlsoKnownAs("GetPatientData")]
        void GetPatientData();

        [AlsoKnownAs("ConvertToString")]
        string ConvertToString(object obj);
    }
}

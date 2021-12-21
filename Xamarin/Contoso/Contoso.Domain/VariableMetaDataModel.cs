using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class VariableMetaDataModel : EntityModelBase
    {
		private int _variableMetaDataId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("VariableMetaData_VariableMetaDataId")]
		public int VariableMetaDataId
		{
			get { return _variableMetaDataId; }
			set
			{
				if (_variableMetaDataId == value)
					return;

				_variableMetaDataId = value;
				OnPropertyChanged();
			}
		}

		private string _data;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("VariableMetaData_Data")]
		public string Data
		{
			get { return _data; }
			set
			{
				if (_data == value)
					return;

				_data = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime _lastUpdated;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("VariableMetaData_LastUpdated")]
		public System.DateTime LastUpdated
		{
			get { return _lastUpdated; }
			set
			{
				if (_lastUpdated == value)
					return;

				_lastUpdated = value;
				OnPropertyChanged();
			}
		}
    }
}
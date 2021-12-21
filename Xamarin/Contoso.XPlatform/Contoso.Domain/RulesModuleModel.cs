using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class RulesModuleModel : EntityModelBase
    {
		private int _rulesModuleId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("RulesModule_RulesModuleId")]
		public int RulesModuleId
		{
			get { return _rulesModuleId; }
			set
			{
				if (_rulesModuleId == value)
					return;

				_rulesModuleId = value;
				OnPropertyChanged();
			}
		}

		private string _name;
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("RulesModule_Name")]
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name == value)
					return;

				_name = value;
				OnPropertyChanged();
			}
		}

		private string _application;
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("RulesModule_Application")]
		public string Application
		{
			get { return _application; }
			set
			{
				if (_application == value)
					return;

				_application = value;
				OnPropertyChanged();
			}
		}

		private byte[] _ruleSetFile;
		[Required]
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("RulesModule_RuleSetFile")]
		public byte[] RuleSetFile
		{
			get { return _ruleSetFile; }
			set
			{
				if (_ruleSetFile == value)
					return;

				_ruleSetFile = value;
				OnPropertyChanged();
			}
		}

		private byte[] _resourceSetFile;
		[Required]
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("RulesModule_ResourceSetFile")]
		public byte[] ResourceSetFile
		{
			get { return _resourceSetFile; }
			set
			{
				if (_resourceSetFile == value)
					return;

				_resourceSetFile = value;
				OnPropertyChanged();
			}
		}

		private string _loggedInUserId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("RulesModule_LoggedInUserId")]
		public string LoggedInUserId
		{
			get { return _loggedInUserId; }
			set
			{
				if (_loggedInUserId == value)
					return;

				_loggedInUserId = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime _lastUpdated;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("RulesModule_LastUpdated")]
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
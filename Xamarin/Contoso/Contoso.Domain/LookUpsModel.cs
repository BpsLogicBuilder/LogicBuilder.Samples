using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class LookUpsModel : EntityModelBase
    {
		private int _lookUpsID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_LookUpsID")]
		public int LookUpsID
		{
			get { return _lookUpsID; }
			set
			{
				if (_lookUpsID == value)
					return;

				_lookUpsID = value;
				OnPropertyChanged();
			}
		}

		private string _text;
		[Required(AllowEmptyStrings = true)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_Text")]
		public string Text
		{
			get { return _text; }
			set
			{
				if (_text == value)
					return;

				_text = value;
				OnPropertyChanged();
			}
		}

		private string _listName;
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_ListName")]
		public string ListName
		{
			get { return _listName; }
			set
			{
				if (_listName == value)
					return;

				_listName = value;
				OnPropertyChanged();
			}
		}

		private string _value;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_Value")]
		public string Value
		{
			get { return _value; }
			set
			{
				if (_value == value)
					return;

				_value = value;
				OnPropertyChanged();
			}
		}

		private double? _numericValue;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_NumericValue")]
		public double? NumericValue
		{
			get { return _numericValue; }
			set
			{
				if (_numericValue == value)
					return;

				_numericValue = value;
				OnPropertyChanged();
			}
		}

		private bool? _booleanValue;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_BooleanValue")]
		public bool? BooleanValue
		{
			get { return _booleanValue; }
			set
			{
				if (_booleanValue == value)
					return;

				_booleanValue = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime? _dateTimeValue;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_DateTimeValue")]
		public System.DateTime? DateTimeValue
		{
			get { return _dateTimeValue; }
			set
			{
				if (_dateTimeValue == value)
					return;

				_dateTimeValue = value;
				OnPropertyChanged();
			}
		}

		private char? _charValue;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_CharValue")]
		public char? CharValue
		{
			get { return _charValue; }
			set
			{
				if (_charValue == value)
					return;

				_charValue = value;
				OnPropertyChanged();
			}
		}

		private System.Guid? _guidValue;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_GuidValue")]
		public System.Guid? GuidValue
		{
			get { return _guidValue; }
			set
			{
				if (_guidValue == value)
					return;

				_guidValue = value;
				OnPropertyChanged();
			}
		}

		private System.TimeSpan? _timeSpanValue;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_TimeSpanValue")]
		public System.TimeSpan? TimeSpanValue
		{
			get { return _timeSpanValue; }
			set
			{
				if (_timeSpanValue == value)
					return;

				_timeSpanValue = value;
				OnPropertyChanged();
			}
		}

		private int _order;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_Order")]
		public int Order
		{
			get { return _order; }
			set
			{
				if (_order == value)
					return;

				_order = value;
				OnPropertyChanged();
			}
		}
    }
}
﻿using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class CheckboxReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public CheckboxReadOnlyObject(
            IUiNotificationService uiNotificationService,
            string name,
            string templateName,
            string checkboxLabel) : base(name, templateName, uiNotificationService)
        {
            CheckboxLabel = checkboxLabel;
        }

        private string _checkboxLabel;
        public string CheckboxLabel
        {
            get => _checkboxLabel;
            [MemberNotNull(nameof(_checkboxLabel))]
            set
            {
                if (_checkboxLabel == value)
                    return;

                _checkboxLabel = value;
                OnPropertyChanged();
            }
        }
    }
}

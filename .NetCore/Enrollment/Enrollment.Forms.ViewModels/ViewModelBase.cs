using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels
{
    public abstract class ViewModelBase
    {
        public string TypeFullName { get { return this.GetType().FullName; } }
        //public List<KeyValuePair<string, string[]>> Errors { get; set; } = new List<KeyValuePair<string, string[]>>();
        //public List<KeyValuePair<string, string[]>> ValidationErrors { get; set; } = new List<KeyValuePair<string, string[]>>();
        //public List<KeyValuePair<string, string[]>> Warnings { get; set; } = new List<KeyValuePair<string, string[]>>();
        //public List<KeyValuePair<string, string[]>> InformationItems { get; set; } = new List<KeyValuePair<string, string[]>>();
        //public ViewModelResult ViewModelResult => GetViewModelResult(ViewModelResult.Success);

        //private ViewModelResult GetViewModelResult(ViewModelResult vmr)
        //{
        //    if (Errors.Count > 0)
        //        vmr = vmr | ViewModelResult.Errors;
        //    else if (ValidationErrors.Count > 0)
        //        vmr = vmr | ViewModelResult.ValidationMessages;
        //    else if (Warnings.Count > 0)
        //        vmr = vmr | ViewModelResult.Warnings;
        //    else if (InformationItems.Count > 0)
        //        vmr = vmr | ViewModelResult.Information;

        //    return vmr;
        //}
    }
}

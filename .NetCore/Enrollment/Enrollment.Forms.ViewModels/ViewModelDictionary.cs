using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels
{
    public class ViewModelDictionary : Dictionary<string, object>
    {
        public new object this[string index]
        {
            get
            {
                if (this.TryGetValue(index, out object val))
                    return val;
                else
                {

                    base.Add(index, (object)Activator.CreateInstance(typeof(ViewModelDictionary).Assembly.GetType(index)));
                    return base[index];
                }
            }
            set
            {
                if (this.ContainsKey(index))
                    base[index] = value;
                else
                    base.Add(index, value);
            }
        }
    }
}

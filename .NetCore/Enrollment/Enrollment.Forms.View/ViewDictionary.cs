using System;
using System.Collections.Generic;

namespace Enrollment.Forms.View
{
    public class ViewDictionary : Dictionary<string, object>
    {
        public new object this[string index]
        {
            get
            {
                if (this.TryGetValue(index, out object val))
                    return val;
                else
                {

                    base.Add(index, Activator.CreateInstance(typeof(ViewDictionary).Assembly.GetType(index)));
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

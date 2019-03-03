using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters
{
    public class ParametersDictionary : Dictionary<string, object>
    {
        public new object this[string index]
        {
            get
            {
                if (this.TryGetValue(index, out object val))
                    return val;
                else
                {

                    base.Add(index, (object)Activator.CreateInstance(typeof(ParametersDictionary).Assembly.GetType(index)));
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

    //public class ParametersDictionary<Tkey, TValue> : Dictionary<Tkey, TValue> where TValue : new()
    //{
    //    public new TValue this[Tkey index]
    //    {
    //        get
    //        {
    //            if (this.TryGetValue(index, out TValue val))
    //                return val;
    //            else
    //            {
    //                base.Add(index, (TValue)Activator.CreateInstance(typeof(ParametersDictionary<,>).Assembly.GetType(index.ToString())));
    //                return base[index];
    //            }
    //        }
    //        set
    //        {
    //            if (this.ContainsKey(index))
    //                base[index] = value;
    //            else
    //                base.Add(index, value);
    //        }
    //    }
    //}
}

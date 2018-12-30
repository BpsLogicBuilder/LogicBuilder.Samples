using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Utils
{
    //public class SafeUpdateDictionary<Tkey, TValue> : Dictionary<Tkey, TValue> where TValue : new()
    //{
    //    public new TValue this[Tkey index]
    //    {
    //        get
    //        {
    //            if (this.TryGetValue(index, out TValue val))
    //                return val;
    //            else
    //            {
    //                base.Add(index, new TValue());
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

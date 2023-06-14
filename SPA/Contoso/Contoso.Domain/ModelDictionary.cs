using System;
using System.Collections.Generic;

namespace Contoso.Domain
{
    public class ModelDictionary : Dictionary<string, BaseModelClass>
    {
        public new BaseModelClass this[string index]
        {
            get
            {
                if (this.TryGetValue(index, out BaseModelClass val))
                    return val;
                else
                {

                    base.Add(index, (BaseModelClass)Activator.CreateInstance(typeof(BaseModelClass).Assembly.GetType(index)));
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

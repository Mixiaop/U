using System.Collections.Generic;

namespace U.Segmentation
{
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Add(key, default(TValue));
                }
                return base[key];
            }
            set { base[key] = value; }
        }
    }
}

using System;

namespace Utilities
{
    /// <summary>
    /// Pair used in <see cref="EnumDictionary{TKey, TValue}">EnumDictionary</see>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class Pair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public Pair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }


    }
}
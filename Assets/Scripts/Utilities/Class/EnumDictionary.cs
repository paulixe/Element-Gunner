using System;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities
{
    /// <summary>
    /// Dictionnary but the key must be of <c>Enum</c> type.
    /// There is only 1 key for each enumeration of the Enum
    /// </summary>
    /// <typeparam name="TKey">key of Enum type</typeparam>
    /// <typeparam name="TValue">object</typeparam>
    [Serializable]
    public class EnumDictionary<TKey, TValue> where TKey : Enum
    {
        [SerializeField] List<Pair<TKey, TValue>> values;

        public EnumDictionary()
        {
            values = new List<Pair<TKey, TValue>>();
            foreach (TKey key in Enum.GetValues(typeof(TKey)))
                values.Add(new Pair<TKey, TValue>(key, default(TValue)));
        }
        public TValue this[TKey key]
        {
            get => GetPair(key).Value;
            set => GetPair(key).Value = value;
        }
        private Pair<TKey, TValue> GetPair(TKey key) =>
            values.Find(p => p.Key.GetHashCode() == key.GetHashCode());

    }
}
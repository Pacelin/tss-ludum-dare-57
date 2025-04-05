using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TSS.Utils;
using UnityEngine;

namespace LudumDare57.Inventory
{
    [CreateSingletonAsset("Assets/_Project/Configs/Inventory Items.asset")]
    public class ItemsCollection : ScriptableObject, IReadOnlyDictionary<string, ItemConfig>
    {
        [SerializedDictionary("Id", "Item")]
        [SerializeField] private SerializedDictionary<string, ItemConfig> _collection;
        public IEnumerator<KeyValuePair<string, ItemConfig>> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => _collection.Count;
        public bool ContainsKey(string key) => _collection.ContainsKey(key);
        public bool TryGetValue(string key, out ItemConfig value) => _collection.TryGetValue(key, out value);
        public ItemConfig this[string key] => _collection[key];
        public IEnumerable<string> Keys => _collection.Keys;
        public IEnumerable<ItemConfig> Values => _collection.Values;
    }
}
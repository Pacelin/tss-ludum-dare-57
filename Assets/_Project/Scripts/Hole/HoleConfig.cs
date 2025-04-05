using System.Collections.Generic;
using TSS.Utils;
using UnityEngine;

namespace LudumDare57.Hole
{
    [CreateSingletonAsset("Assets/_Project/Configs/Hole Config.asset", "Hole Config")]
    public class HoleConfig : ScriptableObject
    {
        public IReadOnlyList<HoleEntryConfig> Entries => _entries;
        public float EntrySpawnThreshold => _entrySpawnThreshold;
        public float InitialHoleSpeed => _initialHoleSpeed;
        
        [SerializeField] private HoleEntryConfig[] _entries;
        [SerializeField] private float _entrySpawnThreshold = 15;
        [SerializeField] private float _initialHoleSpeed = 1;
    }
}
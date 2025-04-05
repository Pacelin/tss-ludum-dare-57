using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LudumDare57.Game;
using TSS.ContentManagement;
using UnityEngine;

namespace LudumDare57.Hole
{
    public class HoleEntries
    {
        private List<HoleEntryConfig> _entries;
        private Queue<HoleEntryConfig> _spawnedEntries;
        private float _lastDepth;

        public void Initialize()
        {
            _entries = new();
            _spawnedEntries = new();
            _entries = CMS.Hole.Config.Entries.OrderBy(e => e.Depth).ToList();
        }

        public void Tick(CancellationToken cancellationToken)
        {
            var currentDepth = GameContext.Hole.Depth.CurrentValue;
            Debug.Log(currentDepth.ToString("0.0"));
            foreach (var entry in _entries)
            {
                if (entry.Depth - CMS.Hole.Config.EntrySpawnThreshold > _lastDepth &&
                    entry.Depth - CMS.Hole.Config.EntrySpawnThreshold <= currentDepth)
                {
                    _spawnedEntries.Enqueue(entry);
                    entry.Spawn();
                }
            }

            if (_spawnedEntries.Count > 0)
            {
                var spawnedEntry = _spawnedEntries.Peek();
                if (spawnedEntry.Depth < _lastDepth &&
                    spawnedEntry.Depth >= currentDepth)
                    _spawnedEntries.Dequeue().StartEntry(cancellationToken);
            }
            _lastDepth = currentDepth;
        }
    }
}
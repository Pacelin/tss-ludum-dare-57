using System.Collections.Generic;
using TSS.Utils.Randoms.Weighted;
using UnityEngine;

namespace LudumDare57.Game
{
    public class LootSpawn : ActivationBounds
    {
        [SerializeField] private float _distance;
        [SerializeField] private Vector2Int _lootCount;
        [SerializeField] private float _spawnSpread = 3;
        [SerializeField] private RandomWeighted<HoleLootsDeposit> _loots;

        private List<GameObject> _loot = new();
        
        protected override void OnActivate()
        {
            var lootCount = Random.Range(_lootCount.x, _lootCount.y + 1);
            var startY = transform.position.y;
            var entry = _loots.CreateInstance().Next();
            var distanceBetween = _distance / lootCount;
            var entryRandom = entry.Entries.CreateInstance();
            for (int i = 0; i < lootCount; i++)
            {
                var loot = entryRandom.Next();
                var spawnX = Random.Range(0, 2) == 0
                    ? GameContext.Hole.LeftWallX
                    : GameContext.Hole.RightWallX;
                _loot.Add(Instantiate(loot, new Vector3(spawnX, startY - distanceBetween * i + Random.Range(-_spawnSpread, _spawnSpread), 0), Quaternion.identity, transform).gameObject);
            }
        }

        protected override void OnDeactivate()
        {
            foreach (var item in _loot)
                if (item)
                    Destroy(item);
            _loot.Clear();
        }

        protected override void OnGizmosSelected()
        {
            var pos = transform.position;
            pos.y -= _distance / 2;
            Gizmos.DrawWireCube(pos, new Vector3(8, _distance, 0.1f));
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using LudumDare57.Game;
using TSS.Utils.Randoms.Weighted;
using UnityEngine;

namespace LudumDare57.Hole
{
    [CreateAssetMenu(menuName = "Game/Hole/Minerals", fileName = "SO_Hole_Minerals")]
    public class HoleLootConfig : HoleEntryConfig
    {
        [SerializeField] private float _lootDistance;
        [SerializeField] private RandomWeighted<HoleLootsComposition> _loots;
        
        public override UniTask StartEntry(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public override void Spawn()
        {
            var entry = _loots.CreateInstance().Next();
            var density = Random.Range(entry.DensityRange.x, entry.DensityRange.y);
            var distanceBetween = _lootDistance / density;
            var entryRandom = entry.Minerals.CreateInstance();
            for (int i = 0; i < density; i++)
            {
                var spawnDepth = Depth + distanceBetween * i;
                var spawnY = GameContext.Hole.GetYForDepth(spawnDepth);
                var loot = entryRandom.Next();
                var spawnX = Random.Range(0, 2) == 0
                        ? GameContext.HoleView.WallsX.x
                        : GameContext.HoleView.WallsX.y;
                Instantiate(loot, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
            }
        }
    }
}
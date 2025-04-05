using TSS.Utils.Randoms.Weighted;
using UnityEngine;

namespace LudumDare57.Hole
{
    [CreateAssetMenu(menuName = "Game/Hole/Loot Composition", fileName = "SO_Loot_Composition")]
    public class HoleLootsComposition : ScriptableObject
    {
        public Vector2Int DensityRange;
        public RandomWeighted<HoleLootView> Minerals;
    }
}
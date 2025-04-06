using TSS.Utils.Randoms.Weighted;
using UnityEngine;

namespace LudumDare57.Game
{
    [CreateAssetMenu(menuName = "Game/LootDeposit", fileName = "SO_LootDeposit")]
    public class HoleLootsDeposit : ScriptableObject
    {
        public RandomWeighted<HoleLootView> Entries;
    }
}
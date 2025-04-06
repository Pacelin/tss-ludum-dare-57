using UnityEngine;

namespace LudumDare57.Game.Shop
{
    [CreateAssetMenu(menuName = "Game/Pickaxe Strength Shop Upgrade", fileName = "SO_ShopPickaxeStrengthUpgrade")]
    public class PickaxeStrengthUpgradeConfig : ShopUpgradeConfig
    {
        [SerializeField] private int _pickaxeStrength = 1;
        public override void OnBuy() => GameContext.PickaxeStrength = _pickaxeStrength;
    }
}
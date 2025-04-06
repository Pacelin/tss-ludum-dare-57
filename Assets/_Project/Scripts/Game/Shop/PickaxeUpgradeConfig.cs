using UnityEngine;

namespace LudumDare57.Game.Shop
{
    [CreateAssetMenu(menuName = "Game/Pickaxe Shop Upgrade", fileName = "SO_ShopPickaxeUpgrade")]
    public class PickaxeUpgradeConfig : ShopUpgradeConfig
    {
        [SerializeField] private int _pickaxeLevel = 1;
        public override void OnBuy() => GameContext.PickaxeLevel = _pickaxeLevel;
    }
}
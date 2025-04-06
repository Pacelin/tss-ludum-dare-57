using UnityEngine;

namespace LudumDare57.Game.Shop
{
    [CreateAssetMenu(menuName = "Game/Income Shop Upgrade", fileName = "SO_ShopIncomeUpgrade")]
    public class IncomeUpgradeConfig : ShopUpgradeConfig
    {
        [SerializeField] private float _incomeMultiplier = 1;
        public override void OnBuy() => GameContext.IncomeMultiplier = _incomeMultiplier;
    }
}
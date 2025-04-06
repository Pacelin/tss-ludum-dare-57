using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class ShopActivation : ActivationTrigger
    {
        [SerializeField] private int _shopLevel;
        [SerializeField] private string _enterAnalytics;
        [SerializeField] private string _exitAnalytics;
        
        protected override void OnActivate()
        {
            GameContext.Shop.Show(_shopLevel, _enterAnalytics, _exitAnalytics);
        }
    }
}
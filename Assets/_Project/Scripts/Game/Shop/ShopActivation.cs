using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class ShopActivation : ActivationTrigger
    {
        [SerializeField] private int _shopLevel;
        
        protected override void OnActivate()
        {
            GameContext.Shop.Show(_shopLevel);
        }
    }
}
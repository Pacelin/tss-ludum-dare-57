using mixpanel;
using UnityEngine;
using UnityEngine.Localization;

namespace LudumDare57.Game.Shop
{
    [CreateAssetMenu(menuName = "Game/Shop Upgrade", fileName = "SO_ShopUpgrade")]
    public class ShopUpgradeConfig : ScriptableObject
    {
        public Sprite Icon => _icon;
        public LocalizedString Name => _name;
        public LocalizedString Description => _description;
        public string ButEvent => _buyEvent;
        public int Cost => _cost;
        public int MinimumShopLevel => _minimumShopLevel;

        [SerializeField] private LocalizedString _name;
        [SerializeField] private LocalizedString _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _buyEvent;
        [SerializeField] private int _cost;
        [SerializeField] private int _minimumShopLevel;

        public virtual void OnBuy() {  }
    }
}
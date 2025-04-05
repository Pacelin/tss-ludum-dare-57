using UnityEngine;
using UnityEngine.Localization;

namespace LudumDare57.Inventory
{
    [CreateAssetMenu(menuName = "Game/Inventory Item", fileName = "SO_Inventory_Item")]
    public class ItemConfig : ScriptableObject
    {
        public Sprite Icon => _icon;
        public LocalizedString Name => _name;
        
        [SerializeField] private Sprite _icon;
        [SerializeField] private LocalizedString _name;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace LudumDare57.Inventory
{
    public class ItemComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public ItemTooltip Tooltip => _tooltip;
        
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemCount;
        [SerializeField] private LocalizedString _itemCountString;
        [SerializeField] private ItemTooltip _tooltip;

        private int _count;

        private void OnEnable()
        {
            _itemCountString.StringChanged += UpdateCountStr;
            UpdateCountStr(_itemCountString.GetLocalizedString());
        }

        private void OnDisable()
        {
            _itemCountString.StringChanged -= UpdateCountStr;
        }

        public void SetIcon(Sprite icon) => _itemIcon.sprite = icon;

        public void SetCount(int count)
        {
            _count = count;
            UpdateCountStr(_itemCountString.GetLocalizedString());
        }

        private void UpdateCountStr(string str)
        {
            _itemCount.gameObject.SetActive(_count > 1);
            _itemCount.text = string.Format(str, _count);
        } 
        
        public void OnPointerEnter(PointerEventData eventData) => _tooltip.Show();
        public void OnPointerExit(PointerEventData eventData) => _tooltip.Hide();
    }
}
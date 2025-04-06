using System;
using LudumDare57.Game;
using LudumDare57.UI;
using R3;
using TMPro;
using TSS.Tweening;
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
        [Space] 
        [SerializeField] private FeedbackButton _soldButton;
        [SerializeField] private ScriptableTween _soldButtonShowTween;
        [SerializeField] private ScriptableTween _soldButtonHideTween;
        
        private int _count;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _itemCountString.StringChanged += UpdateCountStr;
            _soldButton.ObserveClick().Subscribe(_ => GameContext.Inventory.SellItem(this));
            UpdateCountStr(_itemCountString.GetLocalizedString());
        }

        private void OnDisable()
        {
            _itemCountString.StringChanged -= UpdateCountStr;
        }

        public void ShowSoldButton()
        {
            if (_soldButtonHideTween.IsPlaying)
                _soldButtonHideTween.Pause();
            _soldButtonShowTween.Play();
        }

        public void HideSoldButton()
        {
            if (_soldButtonShowTween.IsPlaying)
                _soldButtonShowTween.Pause();
            _soldButtonHideTween.Play();
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
using System;
using R3;
using TMPro;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace LudumDare57.Game.Shop
{
    public class ShopUpgradeComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _buyText;
        [SerializeField] private LocalizedString _butStr;
        [Space] 
        [SerializeField] private Image _iconImage;
        [SerializeField] private LocalizedSprite _soldOutIcon;
        [SerializeField] private ShopUpgradeTooltip _tooltip;
        [Space]
        [SerializeField] private ShopUpgradeConfig[] _items;
        [Space]
        [SerializeField] private ScriptableTween _idleTween;
        [SerializeField] private ScriptableTween _nextTween;

        private int _activeItemIndex;
        private ShopUpgradeConfig _activeItem;
        private IDisposable _disposable;

        private void Awake()
        {
            _activeItemIndex = 0;
            _activeItem = _items[_activeItemIndex];
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
            _disposable = Disposable.Combine(
                GameContext.Coins.Subscribe(_ => UpdateButtonState()),
                Disposable.Create(() => LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged),
                _buyButton.OnClickAsObservable().Subscribe(_ => OnBuyClicked()));
            UpdateItemState();
            _idleTween.Play();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void OnBuyClicked()
        {
            if (_idleTween.IsPlaying)
                _idleTween.Pause();
            GameContext.Coins.Value -= _activeItem.Cost;
            Next();
        }

        public void UpdateButtonState()
        {
            if (_activeItem == null)
            {
                _buyButton.gameObject.SetActive(false);
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _buyText.text = string.Format(_butStr.GetLocalizedString(), _activeItem);
                _buyButton.interactable = GameContext.Coins.CurrentValue >= _activeItem.Cost;
            }
        }

        public void UpdateItemState()
        {
            if (_activeItem == null)
            {
                _iconImage.sprite = _soldOutIcon.LoadAsset();
                _tooltip.Hide();
            }
            else
            {
                _iconImage.sprite = _activeItem.Icon;
                _tooltip.UpdateTooltip(_activeItem.Name.GetLocalizedString(), _activeItem.Description.GetLocalizedString());
            }
        }
        
        private void Next()
        {
            _activeItemIndex++;
            _activeItem = _activeItemIndex >= _items.Length ? null : _items[_activeItemIndex];
            _nextTween.Play();
        }

        private void OnLocaleChanged(Locale locale)
        {
            UpdateButtonState();
            UpdateItemState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_activeItem != null)
                _tooltip.Show();
        }

        public void OnPointerExit(PointerEventData eventData) => _tooltip.Hide();
    }
}
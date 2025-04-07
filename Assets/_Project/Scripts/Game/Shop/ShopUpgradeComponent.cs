using System;
using mixpanel;
using R3;
using TMPro;
using TSS.Audio;
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
        [SerializeField] private GameObject _buyBG;
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
        [SerializeField] private ScriptableTween _nextTween;
        [SerializeField] private RectTransform _sinTarget;
        [SerializeField] private float _sinOffset;
        [SerializeField] private float _sinAmplitude;
        [SerializeField] private float _sinSpeed;

        private int _activeItemIndex;
        private ShopUpgradeConfig _activeItem;
        private IDisposable _disposable;
        private float _startY;

        private void Awake()
        {
            _startY = _sinTarget.anchoredPosition.y;
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
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void Update()
        {
            var pos = _sinTarget.anchoredPosition;
            pos.y = _startY + Mathf.Sin(Time.time * _sinSpeed + _sinOffset) * _sinAmplitude;
            _sinTarget.anchoredPosition = pos;
        }

        private void OnBuyClicked()
        {
            GameContext.Coins.Value -= _activeItem.Cost;
            Mixpanel.Track(_activeItem.ButEvent);
            _activeItem.OnBuy();
            AudioSystem.ItemBuy.PlayOneShot();
            GameContext.Shop.PlayHappy();
            Next();
        }

        public void UpdateButtonState()
        {
            if (_activeItem == null || _activeItem.MinimumShopLevel > GameContext.Shop.ShopLevel)
            {
                _buyButton.gameObject.SetActive(false);
                _buyBG.SetActive(false);
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _buyBG.SetActive(true);
                _buyText.text = string.Format(_butStr.GetLocalizedString(), _activeItem.Cost);
                _buyButton.interactable = GameContext.Coins.CurrentValue >= _activeItem.Cost;
            }
        }

        public void UpdateItemState()
        {
            if (_activeItem == null || _activeItem.MinimumShopLevel > GameContext.Shop.ShopLevel)
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
            UpdateButtonState();
        }

        private void OnLocaleChanged(Locale locale)
        {
            UpdateButtonState();
            UpdateItemState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_activeItem != null && GameContext.Shop.ShopLevel >= _activeItem.MinimumShopLevel)
                _tooltip.Show();
        }

        public void OnPointerExit(PointerEventData eventData) => _tooltip.Hide();
    }
}
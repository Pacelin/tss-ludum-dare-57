using System;
using LudumDare57.UI;
using mixpanel;
using R3;
using TSS.Tweening;
using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class ShopComponent : MonoBehaviour
    {
        public int ShopLevel => _shopLevel;
        
        [SerializeField] private FeedbackButton _hideButton;
        [SerializeField] private ScriptableTween _showTween;
        [SerializeField] private ScriptableTween _hideTween;

        private IDisposable _disposable;
        private int _shopLevel;
        private string _exitAnalytics;

        private void OnEnable()
        {
            _disposable = _hideButton.ObserveClick().Subscribe(_ => Hide());
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        public void Show(int shopLevel, string enterAnalytics, string exitAnalytics)
        {
            _shopLevel = shopLevel;
            if (_hideTween.IsPlaying)
                _hideTween.Pause();
            _showTween.Play();
            GameContext.Inventory.ShowSoldButtons();
            GameContext.Hole.SetStop(true);
            Mixpanel.Track(enterAnalytics);
            _exitAnalytics = exitAnalytics;
        }

        public void Hide()
        {
            _disposable?.Dispose();
            if (_showTween.IsPlaying)
                _showTween.Pause();
            _hideTween.Play();
            GameContext.Inventory.HideSoldButtons();
            GameContext.Hole.SetStop(false);
            Mixpanel.Track(_exitAnalytics);
        }
    }
}
using System;
using LudumDare57.UI;
using R3;
using TSS.Tweening;
using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class ShopComponent : MonoBehaviour
    {
        [SerializeField] private FeedbackButton _hideButton;
        [SerializeField] private ScriptableTween _showTween;
        [SerializeField] private ScriptableTween _hideTween;

        private IDisposable _disposable;

        private void OnEnable()
        {
            _disposable = _hideButton.ObserveClick().Subscribe(_ => Hide());
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        public void Show()
        {
            if (_hideTween.IsPlaying)
                _hideTween.Pause();
            _showTween.Play();
            GameContext.Inventory.ShowSoldButtons();
            GameContext.Hole.SetStop(true);
        }

        public void Hide()
        {
            _disposable?.Dispose();
            if (_showTween.IsPlaying)
                _showTween.Pause();
            _hideTween.Play();
            GameContext.Inventory.HideSoldButtons();
            GameContext.Hole.SetStop(false);
        }
    }
}
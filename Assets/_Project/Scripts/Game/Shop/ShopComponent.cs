using System;
using LudumDare57.UI;
using mixpanel;
using R3;
using TSS.Audio;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare57.Game.Shop
{
    public class ShopComponent : MonoBehaviour
    {
        public int ShopLevel => _shopLevel;
        
        [SerializeField] private FeedbackButton _hideButton;
        [SerializeField] private ScriptableTween _showTween;
        [SerializeField] private ScriptableTween _hideTween;
        [SerializeField] private ScriptableTween _happyTween;

        private IDisposable _disposable;
        private int _shopLevel;
        private string _exitAnalytics;
        private SoundEvent_ShopMusicIn.Instance _soundInstance;
        private SoundEvent_ShopMusicOutside.Instance _outsideInstance;

        public void PlayHappy()
        {
            if (_happyTween.IsPlaying)
                _happyTween.Pause();
            _happyTween.Play();
        }
        
        private void OnEnable()
        {
            _disposable = _hideButton.ObserveClick().Subscribe(_ => Hide());
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        public void Show(int shopLevel, string enterAnalytics, string exitAnalytics, SoundEvent_ShopMusicOutside.Instance outside)
        {
            _outsideInstance = outside;
            _shopLevel = shopLevel;
            if (_hideTween.IsPlaying)
                _hideTween.Pause();
            _showTween.Play();
            GameContext.Inventory.ShowSoldButtons();
            GameContext.Hole.SetStop(true);
            Mixpanel.Track(enterAnalytics);
            _exitAnalytics = exitAnalytics;
            _soundInstance = AudioSystem.ShopMusicIn.CreateInstance();
            _soundInstance.SetTimelinePosition(_outsideInstance.GetTimelinePosition());
            _soundInstance.Start();
            _outsideInstance.SetPaused(true);
            GameContext.Ambient.SetPaused(true);
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
            _outsideInstance.SetTimelinePosition(_soundInstance.GetTimelinePosition());
            _outsideInstance.SetPaused(false);
            GameContext.Ambient.SetPaused(false);
            _soundInstance.Stop(true);
            _soundInstance.Release();
            _soundInstance = null;
        }
    }
}
using System;
using mixpanel;
using R3;
using UnityEngine;

namespace LudumDare57.UI
{
    [RequireComponent(typeof(FeedbackButton))]
    public class SocialButton : MonoBehaviour
    {
        [SerializeField] private FeedbackButton _feedbackButton;
        [SerializeField] private string _url;
        [SerializeField] private string _analytics;
        
        private IDisposable _disposable;

        private void OnValidate()
        {
            if (!_feedbackButton)
                _feedbackButton = GetComponent<FeedbackButton>();
        }

        private void OnEnable()
        {
            _disposable = _feedbackButton.ObserveClick()
                .Subscribe(_ =>
                {
                    Mixpanel.Track("social_" + _analytics);
                    Application.OpenURL(_url);
                });
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
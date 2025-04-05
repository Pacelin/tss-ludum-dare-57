using System;
using R3;
using UnityEngine;

namespace LudumDare57.UI
{
    [RequireComponent(typeof(FeedbackButton))]
    public class SocialButton : MonoBehaviour
    {
        [SerializeField] private FeedbackButton _feedbackButton;
        [SerializeField] private string _url;
        
        private IDisposable _disposable;

        private void OnValidate()
        {
            if (!_feedbackButton)
                _feedbackButton = GetComponent<FeedbackButton>();
        }

        private void OnEnable()
        {
            _disposable = _feedbackButton.ObserveClick()
                .Subscribe(_ => Application.OpenURL(_url));
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
using System;
using System.Threading;
using R3;
using UnityEngine;

namespace LudumDare57.UI
{
    [RequireComponent(typeof(FeedbackButton))]
    public class OpenPauseButton : MonoBehaviour
    {
        [SerializeField] private FeedbackButton _feedbackButton;

        private IDisposable _disposable;

        private void OnValidate()
        {
            if (!_feedbackButton)
                _feedbackButton = GetComponent<FeedbackButton>();
        }

        private void OnEnable()
        {
            _disposable = _feedbackButton.ObserveFeedbackStart().Subscribe(_ =>
                PauseMenu.Open());
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
using R3;
using TSS.ContentManagement;
using TSS.Core;
using TSS.SceneManagement;
using UnityEngine;

namespace LudumDare57.UI
{
    [RequireComponent(typeof(FeedbackButton))]
    public class PlayButton : MonoBehaviour
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
                SceneManager.Scene(CMS.Scenes.Game).Single().Load(Runtime.CancellationToken).Forget());
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}
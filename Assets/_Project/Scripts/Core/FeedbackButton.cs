using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LudumDare57.Core
{
    [PublicAPI]
    public class FeedbackButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UnityEvent _onBecomeHover;
        [SerializeField] private UnityEvent _onClick;
        [SerializeField] private ScriptableTween _toDefaultTween;
        [SerializeField] private ScriptableTween _toHoverTween;
        [SerializeField] private ScriptableTween _toDownTween;
        [SerializeField] private ScriptableTween _feedbackTween;

        private bool _down;
        private bool _hover;
        private readonly Subject<Unit> _clickSubject = new();
        private readonly Subject<Unit> _beforeFeedbackSubject = new();

        public Observable<Unit> ObserveClick() => _clickSubject;
        public Observable<Unit> ObserveFeedbackStart() => _beforeFeedbackSubject;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
            _onBecomeHover?.Invoke();
            UpdateState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
            _down = false;
            UpdateState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            UpdateState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_down)
                return;
            _down = false;
            _beforeFeedbackSubject.OnNext(Unit.Default);
            _onClick?.Invoke();
            if (_feedbackTween)
            {
                KillTransitionTweens();
                _feedbackTween.Play();
                OverlayInputLock.Enable();
                _feedbackTween.WaitWhilePlay().ContinueWith(() =>
                {
                    OverlayInputLock.Disable();
                    _clickSubject.OnNext(Unit.Default);
                });
            }
            else
            {
                UpdateState();
                _clickSubject.OnNext(Unit.Default);
            }
        }

        private void UpdateState()
        {
            if (_feedbackTween && _feedbackTween.IsPlaying)
                return;
            KillTransitionTweens();
            if (_down)
            {
                Debug.Log("Down play");
                _toDownTween.Play();
            }
            else if (_hover)
            {
                Debug.Log("Hover play");
                _toHoverTween.Play();
            }
            else
            {
                Debug.Log("Default play");
                _toDefaultTween.Play();
            }
        }

        private void KillTransitionTweens()
        {
            if (_toDefaultTween.IsPlaying)
                _toDefaultTween.Pause();
            if (_toDownTween.IsPlaying)
                _toDownTween.Pause();
            if (_toHoverTween.IsPlaying)
                _toHoverTween.Pause();
        }
    }
}
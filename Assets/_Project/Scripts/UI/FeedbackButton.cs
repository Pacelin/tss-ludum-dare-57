using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LudumDare57.UI
{
    [PublicAPI]
    [RequireComponent(typeof(Image))]
    public class FeedbackButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        public bool Hold => _down;
        
        [SerializeField] private ScriptableTween _toDefaultTween;
        [SerializeField] private ScriptableTween _toHoverTween;
        [SerializeField] private ScriptableTween _toDownTween;
        [SerializeField] private ScriptableTween _feedbackTween;
        [SerializeField] private bool _lockInputOnFeedback;
        [SerializeField] private UnityEvent _onBecomeHover;
        [SerializeField] private UnityEvent _onClick;

        private bool _down;
        private bool _hover;
        private readonly Subject<Unit> _onDown = new();
        private readonly Subject<Unit> _onUp = new();
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
            if (_down)
                _onUp.OnNext(Unit.Default);
            _down = false;
            UpdateState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            _onDown.OnNext(Unit.Default);
            UpdateState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_down)
                return;
            _down = false;
            _onUp.OnNext(Unit.Default);
            _beforeFeedbackSubject.OnNext(Unit.Default);
            _onClick?.Invoke();
            if (_feedbackTween)
            {
                KillTransitionTweens();
                _feedbackTween.Play();
                if (_lockInputOnFeedback)
                    OverlayInputLock.Enable();
                _feedbackTween.WaitWhilePlay().ContinueWith(() =>
                {
                    if (_lockInputOnFeedback)
                        OverlayInputLock.Disable();
                    UpdateState();
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
                _toDownTween.Play();
            else if (_hover)
                _toHoverTween.Play();
            else
                _toDefaultTween.Play();
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
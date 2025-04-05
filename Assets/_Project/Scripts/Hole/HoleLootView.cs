using Cysharp.Threading.Tasks;
using LudumDare57.Game;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace LudumDare57.Hole
{
    public class HoleLootView : HoleItemView, IPointerClickHandler
    {
        [SerializeField] private ScriptableTween _onClickTween;
        [SerializeField] private ScriptableTween _onDestroyTween;
        [Space]
        [SerializeField] private Vector2Int _costRange;
        [SerializeField] private Vector2Int _kicksCount;
        [SerializeField] private Vector2 _scaleRange;
        [SerializeField] private Vector2 _rotateRange;
        
        private int _cost;
        private int _kicksRemaining;
        
        public void Start()
        {
            _cost = Random.Range(_costRange.x, _costRange.y + 1);
            var t = 0f;
            if (_cost != _costRange.y)
                t = 1f * (_costRange.y - _costRange.x) / (_costRange.y - _cost);
            _kicksRemaining = (int) Mathf.Lerp(_kicksCount.x, _kicksCount.y, t);
            transform.localScale = Vector3.one * Mathf.Lerp(_scaleRange.x, _scaleRange.y, t);

            var rotateAngle = Random.Range(_rotateRange.x, _rotateRange.y);
            var flip = transform.position.x > GameContext.HoleView.HoleCenterX;
            if (flip)
                rotateAngle -= rotateAngle * 2;
            transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, rotateAngle);
        } 

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_kicksRemaining <= 0)
                return;
            if (--_kicksRemaining <= 0)
            {
                _onDestroyTween.Play();
                _onDestroyTween.WaitWhilePlay().ContinueWith(() =>
                {
                    Destroy(gameObject);
                });
            }
            else
            {
                _onClickTween.Play();
            }
        }
    }
}
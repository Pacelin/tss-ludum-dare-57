using System;
using LudumDare57.Game;
using UnityEngine;
using R3;

namespace LudumDare57.Hole
{
    public class HoleItemView : MonoBehaviour
    {
        [SerializeField] private float _yDestroyPosition = 15;

        private float _lastDepth;
        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _lastDepth = GameContext.Hole.Depth.CurrentValue;
            _disposable = GameContext.Hole.Depth.Subscribe(depth =>
            {
                var delta = depth - _lastDepth;
                _lastDepth = depth;
                var pos = transform.position;
                pos.y += delta;
                if (pos.y >= _yDestroyPosition)
                    Destroy(gameObject);
                else if (pos.y <= -_yDestroyPosition)
                    Destroy(gameObject);
                else
                    transform.position = pos;
            });
            OnEnabled();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
            OnDisabled();
        }

        protected virtual void OnEnabled() {}
        protected virtual void OnDisabled() {}
    }
}
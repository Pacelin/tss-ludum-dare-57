using R3;
using UnityEngine;

namespace LudumDare57.Hole
{
    public class HoleModel
    {
        public float DownSpeed
        {
            get => _speed;
            set => _speed = value;
        }

        public ReadOnlyReactiveProperty<float> Depth => _depth;

        private ReactiveProperty<float> _depth;
        private float _speed;

        public HoleModel(float depth, float speed)
        {
            _depth = new ReactiveProperty<float>(depth);
            _speed = speed;
        }

        public void Tick()
        {
            _depth.Value += _speed * Time.deltaTime;
        }

        public float GetYForDepth(float depth)
        {
            var delta = _depth.CurrentValue - depth;
            return delta;
        }
    }
}
using System;
using UnityEngine;

namespace LudumDare57.Game
{
    public class HoleComponent : MonoBehaviour
    {
        [Serializable]
        private struct ParallaxLayer
        {
            public float Parallax;
            public Transform Transform;
        }
        
        public float LeftWallX => _wallsX.x;
        public float RightWallX => _wallsX.y;
        public float HoleCenterX => LeftWallX + (RightWallX - LeftWallX) / 2;

        [SerializeField] private Vector2 _wallsX;
        [SerializeField] private ParallaxLayer[] _parallaxLayers;

        private float _depth = 0;
        private float _downSpeed = 1;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(LeftWallX, -1000, 0), new Vector3(LeftWallX, 1000, 0));
            Gizmos.DrawLine(new Vector3(RightWallX, -1000, 0), new Vector3(RightWallX, 1000, 0));
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawLine(new Vector3(RightWallX, -1000, 0), new Vector3(RightWallX, 1000, 0));
        }

        private void Update()
        {
            _depth += _downSpeed * Time.deltaTime;
            foreach (var layer in _parallaxLayers)
            {
                var t = layer.Transform;
                var pos = t.localPosition;
                pos.y = _depth * layer.Parallax;
                t.localPosition = pos;
            }
        }
    }
}
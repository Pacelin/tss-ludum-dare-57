using System;
using LudumDare57.Game;
using UnityEngine;
using R3;

namespace LudumDare57.Hole
{
    public class HoleView : MonoBehaviour
    {
        public Vector2 WallsX => _wallsX;
        public float HoleCenterX => _holeCenterX;
        
        [SerializeField] private Material _leftWallMaterial;
        [SerializeField] private Material _rightWallMaterial;
        [SerializeField] private float _holeCenterX;
        [SerializeField] private Vector2 _wallsX;
        
        private static readonly int VerticalOffset = Shader.PropertyToID("_Vertical_Offset");
        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable = GameContext.Hole.Depth.Subscribe(depth =>
            {
                _leftWallMaterial.SetFloat(VerticalOffset, depth);
                _rightWallMaterial.SetFloat(VerticalOffset, depth);
            });
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(new Vector3(_wallsX.x, -15, 0), new Vector3(_wallsX.x, 15, 0));
            Gizmos.DrawLine(new Vector3(_wallsX.y, -15, 0), new Vector3(_wallsX.y, 15, 0));
        }
    }
}
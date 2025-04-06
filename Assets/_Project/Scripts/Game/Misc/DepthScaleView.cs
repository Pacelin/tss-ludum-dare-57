using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class DepthScaleView : MonoBehaviour
    {
        [SerializeField] private RectTransform _scroll;
        [SerializeField] private float _pixelsPerUnit;

        private void Update()
        {
            var ancPos = _scroll.anchoredPosition;
            ancPos.y = GameContext.Hole.Depth * _pixelsPerUnit;
            _scroll.anchoredPosition = ancPos;
        }
    }
}
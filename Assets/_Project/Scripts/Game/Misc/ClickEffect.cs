using TSS.Core;
using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class ClickEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _system;

        private static ClickEffect _effect;
        
        private void Awake() => _effect = this;

        public static void Play()
        {
            var mousePos = SceneCameraProvider.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            _effect.transform.position = mousePos;
            _effect._system.Play();
        }
    }
}
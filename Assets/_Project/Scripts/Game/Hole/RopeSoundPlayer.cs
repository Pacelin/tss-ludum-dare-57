using TSS.Audio;
using UnityEngine;

namespace LudumDare57.Game
{
    public class RopeSoundPlayer : MonoBehaviour
    {
        [SerializeField] private Vector2 _cooldownRange;
        private float _cooldown;

        private void Awake()
        {
            _cooldown = Random.Range(_cooldownRange.x, _cooldownRange.y);
        }

        private void Update()
        {
            if (!GameContext.Hole)
                return;
            if (GameContext.Hole.IsStopped)
                return;
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0)
            {
                AudioSystem.RopeMove.PlayOneShot();
                _cooldown = Random.Range(_cooldownRange.x, _cooldownRange.y);
            }
        }
    }
}
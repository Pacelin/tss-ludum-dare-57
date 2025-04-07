using DG.Tweening;
using TSS.Core;
using UnityEngine;

namespace LudumDare57.Game.Tutorial
{
    public class TutorialActivation : ActivationBounds
    {
        [SerializeField] private ParticleSystem _cursorParticle;
        [SerializeField] private Transform _cursor;
        [SerializeField] private HoleLootView _lootPrefab;

        private HoleLootView _loot;
        private bool _started;

        private void OnDisable() => DOTween.Kill(this);

        protected override void OnActivate()
        {
            var t = transform;
            var y = t.position.y;
            var x = GameContext.Hole.RightWallX;
            _loot = Instantiate(_lootPrefab, new Vector3(x, y), Quaternion.identity, t);
        }

        protected override void OnDeactivate()
        {
            if (_loot)
                Destroy(_loot.gameObject);
        }

        protected override void OnGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            var pos = transform.position;
            Gizmos.DrawLine(new Vector3(-4, pos.y), new Vector3(4, pos.y));
            Gizmos.color = Color.white;
        }

        protected override void OnUpdate()
        {
            if (!_started && transform.position.y >= -SceneCameraProvider.MainCamera.orthographicSize)
            {
                if (!_loot)
                {
                    Destroy(gameObject);
                    return;
                }
                _started = true;
                GameContext.Hole.SetStop(true);
                _cursor.gameObject.SetActive(true);
                DOTween.Sequence(this)
                    .AppendInterval(0.5f)
                    .Append(_cursor.DOScale(0.7f, 0.1f))
                    .AppendCallback(() => _cursorParticle.Play())
                    .Append(_cursor.DOScale(1f, 0.1f))
                    .Append(_cursor.DOScale(0.7f, 0.1f))
                    .AppendCallback(() => _cursorParticle.Play())
                    .Append(_cursor.DOScale(1f, 0.1f))
                    .AppendInterval(0.5f)
                    .SetLoops(-1)
                    .Play();
            }
            else if (_started)
            {
                if (!_loot)
                {
                    GameContext.Hole.SetStop(false);
                    Destroy(gameObject);
                }
            }
        }
    }
}
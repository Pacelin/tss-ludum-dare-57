using System;
using DG.Tweening;
using TSS.ContentManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudumDare57.Game.Shop
{
    public class DropView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Space] 
        [SerializeField] private float _fallDelay;
        [SerializeField] private float _yFall;
        [SerializeField] private float _fallDuration;
        [SerializeField] private Ease _fallEase;
        [Space] 
        [SerializeField] private float _jumpStrength;
        [SerializeField] private float _jumpDelay;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private Ease _jumpEase;
        [Space] 
        [SerializeField] private float _sideStrength;
        [SerializeField] private float _sideSpread;
        [SerializeField] private float _sideDuration;
        [SerializeField] private Ease _sideEase;
        [Space]
        [SerializeField] private ParticleSystem _claimParticle;
        [SerializeField] private ParticleSystem[] _flyParticles;

        private void OnDisable()
        {
            DOTween.Kill(this);
        }

        public void Claim(string item, int cost)
        {
            var tPos = transform.position;
            _spriteRenderer.sprite = CMS.InventoryItems[item].Icon;
            var directionX = Mathf.Sign(GameContext.Hole.HoleCenterX - tPos.x);
            var vector = new Vector3(_sideStrength * directionX, 0, 0);
            var withSpread = Quaternion.Euler(0, 0, Random.Range(-_sideSpread, _sideSpread)) * vector;
            var sidePosition = tPos + withSpread;
            if (GameContext.Inventory.HasSpaceFor(item))
                PlayClaim(sidePosition, item, cost);
            else
                PlayFall(sidePosition);
        }

        private void PlayClaim(Vector3 sidePos, string item, int cost)
        {
            DOTween.Sequence(this)
                .Append(transform.DOMove(sidePos, _sideDuration).SetEase(_sideEase))
                .AppendCallback(GameContext.Player.OpenBackpack)
                .AppendInterval(_jumpDelay)
                .AppendCallback(() => transform.SetParent(GameContext.Player.BackpackPoint))
                .Append(transform.DOLocalJump(Vector3.zero, _jumpStrength, 1, _jumpDuration).SetEase(_jumpEase))
                .AppendCallback(() =>
                {
                    foreach (var flyParticle in _flyParticles)
                    {
                        flyParticle.Stop();
                        flyParticle.Clear();
                    }
                    _spriteRenderer.gameObject.SetActive(false);
                    _claimParticle.Play();
                    GameContext.Inventory.TryAddItem(item, cost);
                })
                .AppendCallback(GameContext.Player.CloseBackpack)
                .AppendInterval(_claimParticle.main.duration)
                .AppendCallback(() => Destroy(gameObject))
                .Play();
        }

        private void PlayFall(Vector3 sidePos)
        {
            var fallPos = sidePos;
            fallPos.y = _yFall;
            DOTween.Sequence(this)
                .Append(transform.DOMove(sidePos, _sideDuration).SetEase(_sideEase))
                .AppendInterval(_fallDelay)
                .Append(transform.DOMove(fallPos, _fallDuration).SetEase(_fallEase))
                .AppendInterval(2)
                .AppendCallback(() => Destroy(gameObject))
                .Play();
        }
    }
}
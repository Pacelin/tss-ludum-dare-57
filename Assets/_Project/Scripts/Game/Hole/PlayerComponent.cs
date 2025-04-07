using DG.Tweening;
using UnityEngine;

namespace LudumDare57.Game
{
    public class PlayerComponent : MonoBehaviour
    {
        public Transform BackpackPoint => _backpackPoint;
        public Animator Animator => _animator;

        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _backpack;
        [SerializeField] private Transform _backpackPoint;
        [SerializeField] private Transform _backpackCap;
        [SerializeField] private Vector3 _backpackLocalClosedRotation;
        [SerializeField] private Vector3 _backpackLocalOpenedRotation;
        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;
        [SerializeField] private Ease _openEase;
        [SerializeField] private Ease _closeEase;
        [SerializeField] private float _kickOutDuration;
        [SerializeField] private float _kickOutY;

        private void OnDisable()
        {
            DOTween.Kill(this);
        }

        public void OpenBackpack()
        {
            DOTween.Kill(this);
            _backpackCap.DOLocalRotate(_backpackLocalOpenedRotation, _openDuration)
                .SetEase(_openEase).SetTarget(this)
                .Play();
        }

        public void CloseBackpack()
        {
            DOTween.Kill(this);
            _backpackCap.DOLocalRotate(_backpackLocalClosedRotation, _closeDuration)
                .SetEase(_closeEase).SetTarget(this)
                .Play();
        }

        public void KickOutBackpack()
        {
            var bpPos = _backpack.position;
            bpPos.y = _kickOutY;
            bpPos.x += 1f;
            DOTween.Kill(this);
            DOTween.Sequence(this)
                .AppendCallback(() => _backpack.SetParent(null))
                .Append(_backpack.DOJump(bpPos, 3, 1, _kickOutDuration).SetEase(Ease.InQuad))
                .Join(_backpack.DOShakeRotation(_kickOutDuration / 2, new Vector3(0, 0, 10), 5))
                .Play();
        }
    }
}
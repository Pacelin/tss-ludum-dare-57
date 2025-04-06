using System;
using DG.Tweening;
using UnityEngine;

namespace LudumDare57.Game
{
    public class PlayerComponent : MonoBehaviour
    {
        public Transform BackpackPoint => _backpackPoint;
        
        [SerializeField] private Transform _backpackPoint;
        [SerializeField] private Transform _backpackCap;
        [SerializeField] private Vector3 _backpackLocalClosedRotation;
        [SerializeField] private Vector3 _backpackLocalOpenedRotation;
        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;
        [SerializeField] private Ease _openEase;
        [SerializeField] private Ease _closeEase;

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
    }
}
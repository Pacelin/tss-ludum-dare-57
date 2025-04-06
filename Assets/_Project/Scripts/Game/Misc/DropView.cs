using System;
using DG.Tweening;
using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class DropView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Space]
        [SerializeField] private float _yFall;
        [SerializeField] private float _fallDuration;
        [SerializeField] private Ease _fallEase;
        [Space] 
        [SerializeField] private float _jumpDuration;
        [SerializeField] private Ease _jumpEase;
        [Space] 
        [SerializeField] private float _sideStrength;
        [SerializeField] private float _sideSpread;
        [SerializeField] private float _sideDuration;
        [SerializeField] private Ease _sideEase;
        [Space]
        [SerializeField] private float _jumpStrength;
        [SerializeField] private float _closedAngle;
        [SerializeField] private float _openedAngle;
        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;
        [SerializeField] private Ease _openEase;
        [SerializeField] private Ease _closeEase;
        [Space] 
        [SerializeField] private ParticleSystem _claimParticle;

        private Tween _tween;
        
        public void PlaySide(Action finishedCallback)
        {
            //_tween
        }
    }
}
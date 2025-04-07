
using UnityEngine;

namespace LudumDare57.Game
{
    public class VfxEffectTrigger : ActivationTrigger
    {
        private enum Behaviour
        {
            Play,
            Stop,
        }
        
        [SerializeField] private VFXEffect _effect;

        [SerializeField]
        private Behaviour _effectBehaviour;
        
        protected override void OnActivate()
        {
            if (_effectBehaviour == Behaviour.Play)
                _effect.PlayEffect();
            else
                _effect.StopEffect();
        }
    }
}

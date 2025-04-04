#if TSS_FMOD
using DG.Tweening;
using TSS.Audio;
using UnityEngine;

namespace TSS.Tweening
{
    [System.Serializable]
    [ScriptableTweenPath("FMOD Sound", 101)]
    public class ScriptableTweenFMOD_DOSound : IScriptableTweenItemNoTarget
    {
        [SerializeField] private ETweenConnectBehaviour _connectBehaviour;
        [Box("FMOD")]
        [Order(10)] 
        [SerializeField]
        private SoundEvent _sound;

        public void AddTween(Sequence sequence)
        {
            if (_connectBehaviour == ETweenConnectBehaviour.Append)
                sequence.AppendCallback(() => _sound.PlayOneShot());
            else
                sequence.JoinCallback(() => _sound.PlayOneShot());
        }
    }
}
#endif
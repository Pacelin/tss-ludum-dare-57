using System;
using TSS.Audio;
using UnityEngine;

namespace LudumDare57.Audio
{
    public class LifetimedOST : MonoBehaviour
    {
        [SerializeField] private SoundEvent _event;

        private SoundEvent.Instance _instance;
        private void OnEnable()
        {
            _instance = _event.CreateInstance();
            _instance.Start();
        }

        private void OnDisable()
        {
            _instance.Stop(true);
            _instance.Release();
            _instance = null;
        }
    }
}
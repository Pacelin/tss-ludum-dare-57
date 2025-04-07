using TSS.Audio;
using UnityEngine;

namespace LudumDare57.Game.Shop
{
    public class ShopActivation : ActivationTrigger
    {
        [SerializeField] private int _shopLevel;
        [SerializeField] private string _enterAnalytics;
        [SerializeField] private string _exitAnalytics;
        private SoundEvent_ShopMusicOutside.Instance _soundInstance;

        private void OnEnable()
        {
            _soundInstance = AudioSystem.ShopMusicOutside.CreateInstance();
            _soundInstance.AttachTo(gameObject);
            _soundInstance.Start();
        }

        private void OnDisable()
        {
            _soundInstance.Stop(false);
            _soundInstance.Release();
            _soundInstance = null;
        }

        protected override void OnActivate()
        {
            GameContext.Shop.Show(_shopLevel, _enterAnalytics, _exitAnalytics, _soundInstance);
        }
    }
}
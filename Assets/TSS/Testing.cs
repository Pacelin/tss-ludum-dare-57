using TSS.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace TSS.ContentManagement
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private Button _first;
        [SerializeField] private Button _second;
        [SerializeField] private Slider _slider;
        [SerializeField] private Slider _pitchSlider;

        private SoundEvent_OST.Instance _ost;

        private void OnEnable()
        {
            _slider.value = AudioSystem.Volumes.MasterVolume;
            _pitchSlider.value = AudioSystem.Global.GetMusicPitch();
            _first.onClick.AddListener(OnFirstClick);
            _second.onClick.AddListener(OnSecondClick);
            _slider.onValueChanged.AddListener(OnVolumeChanged);
            _pitchSlider.onValueChanged.AddListener(OnPitchChanged);
            _ost = AudioSystem.OST.CreateInstance();
            _ost.Start();
        }

        private void OnDisable()
        {
            _first.onClick.RemoveListener(OnFirstClick);
            _second.onClick.RemoveListener(OnSecondClick);
            _slider.onValueChanged.RemoveListener(OnVolumeChanged);
            _pitchSlider.onValueChanged.RemoveListener(OnPitchChanged);
            _ost.Stop(false);
            _ost.Release();
            _ost = null;
        }

        private void OnFirstClick() =>
            _ost.SetMusic(SoundEvent_OST.ELabel_Music.Default);
        private void OnSecondClick() =>
            _ost.SetMusic(SoundEvent_OST.ELabel_Music.Game);

        private void OnVolumeChanged(float volume) =>
            AudioSystem.Volumes.MasterVolume = volume;
        private void OnPitchChanged(float pitch) =>
            AudioSystem.Global.SetMusicPitch(pitch);
    }
}

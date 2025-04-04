using mixpanel;
using TSS.Audio;
using TSS.Utils.Randoms.ADHOC;
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
        [SerializeField] private RandomADHOC<ScriptableObject> _r;

        private SoundEvent_OST.Instance _ost;
        private bool _isFirst;

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
            Mixpanel.StartTimedEvent("first_music");
            _isFirst = true;
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

        private void OnFirstClick()
        {
            if (_isFirst)
                return;
            _isFirst = true;
            _ost.SetMusic(SoundEvent_OST.ELabel_Music.Default);
            Mixpanel.Track("second_music");
            Mixpanel.StartTimedEvent("first_music");
        }

        private void OnSecondClick()
        {
            if (!_isFirst)
                return;
            _isFirst = false;
            _ost.SetMusic(SoundEvent_OST.ELabel_Music.Game);
            Mixpanel.Track("first_music");
            //Mixpanel.ClearTimedEvent("first_music");
            Mixpanel.StartTimedEvent("second_music");
        }

        private void OnVolumeChanged(float volume) =>
            AudioSystem.Volumes.MasterVolume = volume;
        private void OnPitchChanged(float pitch) =>
            AudioSystem.Global.SetMusicPitch(pitch);
    }
}

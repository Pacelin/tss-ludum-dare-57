using TSS.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare57.UI
{
    public class VolumesView : MonoBehaviour
    {
        [SerializeField] private Slider _master;
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _sfx;

        private void OnEnable()
        {
            _master.SetValueWithoutNotify(AudioSystem.Volumes.MasterVolume);
            _music.SetValueWithoutNotify(AudioSystem.Volumes.GetVolume(0));
            _sfx.SetValueWithoutNotify(AudioSystem.Volumes.GetVolume(1));
            _master.onValueChanged.AddListener(OnMasterChanged);
            _music.onValueChanged.AddListener(OnMusicChanged);
            _sfx.onValueChanged.AddListener(OnSFXChanged);
        }

        private void OnDisable()
        {
            _master.onValueChanged.RemoveListener(OnMasterChanged);
            _music.onValueChanged.RemoveListener(OnMusicChanged);
            _sfx.onValueChanged.RemoveListener(OnSFXChanged);
        }

        private void OnMasterChanged(float value) => AudioSystem.Volumes.MasterVolume = value;
        private void OnMusicChanged(float value) => AudioSystem.Volumes.SetVolume(0, value);
        private void OnSFXChanged(float value) => AudioSystem.Volumes.SetVolume(1, value);
    }
}
using TMPro;
using TSS.Tweening;
using UnityEngine;

namespace LudumDare57.Game.GameEvent
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ScriptableTween _showTween;
        [SerializeField] private ScriptableTween _hideTween;

        public void Show(string text)
        {
            _text.text = text;
            if (_hideTween.IsPlaying)
                _hideTween.Pause();
            _showTween.Play();
        }

        public void Hide()
        {
            if (_showTween.IsPlaying)
                _showTween.Pause();
            _hideTween.Play();
        }
    }
}
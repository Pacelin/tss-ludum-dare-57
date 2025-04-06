using TMPro;
using TSS.Tweening;
using UnityEngine;

namespace LudumDare57.Game.GameEvent
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ScriptableTween _showTween;

        public void Show(string text)
        {
            _text.text = text;
            _showTween.Play();
        }
    }
}
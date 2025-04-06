using System;
using TMPro;
using UnityEngine;
using R3;

namespace LudumDare57.Game.Shop
{
    [RequireComponent(typeof(TMP_Text))]
    public class CoinsText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private IDisposable _disposable;
        
        private void OnValidate()
        {
            if (!_text)
                _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _disposable = GameContext.Coins.Subscribe(c => _text.text = c.ToString());
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
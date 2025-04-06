using System.Collections.Generic;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace LudumDare57.Game.GameEvent
{
    public class GameEventOptionComponent : MonoBehaviour
    {
        public Button Button => _button;
        public string SelectionAnalytics => _selectionAnalytics;
        public IReadOnlyList<GameEventItemPenalty> Penalty => _penalty;
        public int CoinsPenalty => _coinsPenalty;
        public IReadOnlyList<GameEventItemRequire> Requires => _requires;
        public LocalizedString Result => _result;
        
        [SerializeField] private Button _button;
        [SerializeField] private string _selectionAnalytics;
        [SerializeField] private GameEventItemPenalty[] _penalty;
        [SerializeField] private int _coinsPenalty = 0;
        [SerializeField] private GameEventItemRequire[] _requires;
        [SerializeField] private LocalizedString _result;
    }
}
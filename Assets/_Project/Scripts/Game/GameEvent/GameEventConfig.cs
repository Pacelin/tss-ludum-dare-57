using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare57.Game.GameEvent
{
    public class GameEventOptionComponent : MonoBehaviour
    {
        public Observable<Unit> OnClick => _button.OnClickAsObservable();
        public string SelectionAnalytics => _selectionAnalytics;
        public IReadOnlyList<GameEventItemPenalty> Penalty => _penalty;
        public IReadOnlyList<GameEventItemRequire> Requires => _requires;

        [SerializeField] private Button _button;
        [SerializeField] private string _selectionAnalytics;
        [SerializeField] private GameEventItemPenalty[] _penalty;
        [SerializeField] private GameEventItemRequire[] _requires;
    }
}
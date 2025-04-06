using System.Linq;
using Cysharp.Threading.Tasks;
using mixpanel;
using R3;
using TMPro;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare57.Game.GameEvent
{
    public class GameEventComponent : MonoBehaviour
    {
        [SerializeField] private GameEventOptionComponent[] _options;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private ScriptableTween _openTween;
        [SerializeField] private ScriptableTween _showResultTween;
        [SerializeField] private ScriptableTween _hideTween;
        [SerializeField] private Button _clickToContinue;
        
        private CompositeDisposable _disposables;
        private string _eventAnalytics;
        private bool _selected;
        
        private void OnEnable()
        {
            _selected = false;
            _disposables = new();
            foreach (var option in _options)
                StartOption(option);
            _clickToContinue.OnClickAsObservable().Subscribe(_ =>
            {
                if (!_selected)
                    return;
                if (_showResultTween.IsPlaying)
                    return;
                Mixpanel.Track(_eventAnalytics);
                GameContext.Hole.SetStop(false);
                _hideTween.Play();
                _hideTween.WaitWhilePlay().ContinueWith(() => Destroy(gameObject)).Forget();
            }).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables?.Dispose();
        }

        public void StartEvent(string eventAnalytics)
        {
            GameContext.Hole.SetStop(true);
            _eventAnalytics = eventAnalytics;
            Mixpanel.StartTimedEvent(eventAnalytics);
            _openTween.Play();
        }

        private void StartOption(GameEventOptionComponent option)
        {
            bool enableOption = true;
            var items = GameContext.Inventory.GetAvailableItems();
            foreach (var require in option.Requires)
            {
                if (items.Any(item => item.id == require.Item && item.count >= require.MinimumCount))
                    continue;
                enableOption = false;
                break;
            }

            option.Button.interactable = enableOption;
            option.Button.OnClickAsObservable().Subscribe(_ =>
            {
                if (_selected)
                    return;
                if (_openTween.IsPlaying)
                    return;
                _selected = true;
                if (option.CoinsPenalty > 0)
                    GameContext.Coins.Value = Mathf.Max(0, GameContext.Coins.Value - option.CoinsPenalty);
                foreach (var itemPenalty in option.Penalty)
                {
                    if (items.Length == 0)
                        continue;
                    if (itemPenalty.AnyItem)
                    {
                        GameContext.Inventory.RemoveItem(items[^1].id, itemPenalty.Count);
                        items = GameContext.Inventory.GetAvailableItems();
                    }
                    else if (items.Any(item => item.id == itemPenalty.Item))
                    {
                        GameContext.Inventory.RemoveItem(itemPenalty.Item, itemPenalty.Count);
                    }
                }
                if (_openTween.IsPlaying)
                    _openTween.Pause();

                _resultText.text = option.Result.GetLocalizedString();
                _showResultTween.Play();
                Mixpanel.Track(option.SelectionAnalytics);
            }).AddTo(_disposables);
        }
    }
}
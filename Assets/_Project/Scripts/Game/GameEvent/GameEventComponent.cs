using System.Linq;
using mixpanel;
using R3;
using TSS.Tweening;
using UnityEngine;

namespace LudumDare57.Game.GameEvent
{
    public class GameEventComponent : MonoBehaviour
    {
        [SerializeField] private GameEventOptionComponent[] _options;
        [SerializeField] private ScriptableTween _openTween;
        
        private CompositeDisposable _disposables;
        private string _eventAnalytics;
        
        private void OnEnable()
        {
            _disposables = new();
            foreach (var option in _options)
                StartOption(option);
        }

        private void OnDisable()
        {
            _disposables?.Dispose();
        }

        public void StartEvent(string eventAnalytics)
        {
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
                option.ShowResultTween.Play();
                Mixpanel.Track(option.SelectionAnalytics);
                Mixpanel.Track(_eventAnalytics);
            }).AddTo(_disposables);
        }
    }
}
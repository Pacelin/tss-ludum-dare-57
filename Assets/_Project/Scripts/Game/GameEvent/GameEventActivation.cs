using UnityEngine;

namespace LudumDare57.Game.GameEvent
{
    public class GameEventActivation : ActivationTrigger
    {
        [SerializeField] private GameEventComponent _eventPrefab;
        [SerializeField] private string _eventAnalytics;
        
        protected override void OnActivate()
        {
            var component = Instantiate(_eventPrefab);
            component.StartEvent(_eventAnalytics);
        }
    }
}
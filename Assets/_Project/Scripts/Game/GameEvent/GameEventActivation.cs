using UnityEngine;

namespace LudumDare57.Game.GameEvent
{
    public class GameEventActivation : ActivationTrigger
    {
        [SerializeField] private GameEventComponent _eventConfig;
        [SerializeField] private string _eventStartAnalytics;
        [SerializeField] private string _eventEndAnalytics;
        protected override void OnActivate()
        {
            throw new System.NotImplementedException();
        }
    }
}
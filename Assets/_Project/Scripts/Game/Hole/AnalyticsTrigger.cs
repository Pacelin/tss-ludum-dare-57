using mixpanel;
using UnityEngine;

namespace LudumDare57.Game
{
    public class AnalyticsTrigger : ActivationTrigger
    {
        [SerializeField] private string _event;
        
        protected override void OnActivate() => Mixpanel.Track(_event);
    }
}
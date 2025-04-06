using UnityEngine;
using UnityEngine.Localization;

namespace LudumDare57.Game.GameEvent
{
    public class GameLocationActivation : ActivationTrigger
    {
        [SerializeField] private LocalizedString _locationName;

        protected override void OnActivate() => GameContext.LocationView.Show(_locationName.GetLocalizedString());
    }
}
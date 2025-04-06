using UnityEngine.Localization;

namespace LudumDare57.Game.GameEvent
{
    [System.Serializable] 
    public class GameEventOption
    {
        public LocalizedString Text;
        public string SelectionAnalytics;
        public GameEventItemPenalty[] Penalty;
        public GameEventItemRequire[] Requires;
    }
}
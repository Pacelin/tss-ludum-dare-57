using LudumDare57.Inventory;

namespace LudumDare57.Game.GameEvent
{
    [System.Serializable] 
    public class GameEventItemRequire
    {
        [InventoryItem]
        public string Item;
        public int MinimumCount;
    }
}
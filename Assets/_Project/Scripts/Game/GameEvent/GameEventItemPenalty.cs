using LudumDare57.Inventory;

namespace LudumDare57.Game.GameEvent
{
    [System.Serializable] 
    public class GameEventItemPenalty
    {
        public bool AnyItem;
        [InventoryItem]
        public string Item;
        public int Count;
    }
}
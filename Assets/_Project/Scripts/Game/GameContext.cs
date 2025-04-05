using System.Threading;
using LudumDare57.Inventory;

namespace LudumDare57.Game
{
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        public static GameStateMachine StateMachine { get; set; }
        
        public static HoleComponent Hole { get; set; }
        public static PlayerComponent Player { get; set; }
        public static InventoryComponent Inventory { get; set; }
    }
}
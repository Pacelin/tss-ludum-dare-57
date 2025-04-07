using System.Threading;
using LudumDare57.Game.GameEvent;
using LudumDare57.Game.Shop;
using LudumDare57.Inventory;
using R3;
using TSS.Audio;

namespace LudumDare57.Game
{
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        public static GameStateMachine StateMachine { get; set; }
        
        public static HoleComponent Hole { get; set; }
        public static PlayerComponent Player { get; set; }
        public static InventoryComponent Inventory { get; set; }
        public static ShopComponent Shop { get; set; }
        public static LocationView LocationView { get; set; }
        public static DepthScaleView DepthScale { get; set; }
        public static SoundEvent_Ambient.Instance Ambient { get; set; }

        public static ReactiveProperty<int> Coins { get; } = new();
        public static int PickaxeLevel { get; set; }
        public static int PickaxeStrength { get; set; }
        public static float IncomeMultiplier { get; set; }
    }
}
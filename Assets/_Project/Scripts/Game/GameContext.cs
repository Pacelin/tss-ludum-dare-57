using System.Threading;
using LudumDare57.Hole;

namespace LudumDare57.Game
{
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        public static GameStateMachine StateMachine { get; set; }
        
        public static HoleModel Hole { get; set; }
        public static HoleEntries HoleEntries { get; set; }
        public static HoleView HoleView { get; set; }
        
        public static PlayerView PlayerView { get; set; }
    }
}
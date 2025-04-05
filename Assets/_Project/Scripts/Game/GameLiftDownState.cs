using System.Threading;
using Cysharp.Threading.Tasks;

namespace LudumDare57.Game
{
    public class GameLiftDownState : GameState
    {
        private CancellationToken _cancellationToken;
        
        public override UniTask Start(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            return UniTask.Never(cancellationToken);
        }

        public override void Update()
        {
            GameContext.HoleEntries.Tick(_cancellationToken);
            GameContext.Hole.Tick();
        }
    }
}
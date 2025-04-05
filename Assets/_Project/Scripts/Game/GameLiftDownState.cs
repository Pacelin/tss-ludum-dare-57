using System.Threading;
using Cysharp.Threading.Tasks;

namespace LudumDare57.Game
{
    public class GameLiftDownState : GameState
    {
        public override UniTask Start(CancellationToken cancellationToken)
        {
            return UniTask.Never(cancellationToken);
        }

        public override void Update()
        {
        }
    }
}
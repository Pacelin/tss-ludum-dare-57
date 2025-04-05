using System.Threading;
using Cysharp.Threading.Tasks;

namespace LudumDare57.Game
{
    public abstract class GameState
    {
        public abstract UniTask Start(CancellationToken cancellationToken);
        public abstract void Update();

        protected void NextTo(GameState state) =>
            GameContext.StateMachine.SwitchTo(state);
    }
}
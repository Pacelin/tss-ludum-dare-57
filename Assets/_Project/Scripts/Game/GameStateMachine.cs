using System.Threading;
using Cysharp.Threading.Tasks;
using TSS.Core;

namespace LudumDare57.Game
{
    public class GameStateMachine
    {
        private GameState _currentState;
        
        public void Run()
        {
            _currentState = new GameLiftDownState();
            _currentState.Start(GameContext.CancellationToken).Forget();
        }

        public void Update()
        {
            _currentState?.Update();
        }
        
        public void SwitchTo(GameState state)
        {
            if (!GameContext.CancellationToken.IsCancellationRequested)
            {
                _currentState = state;
                _currentState.Start(GameContext.CancellationToken).Forget();
            }
        }
    }
}